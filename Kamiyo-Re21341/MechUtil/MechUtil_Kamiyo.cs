using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using LOR_DiceSystem;

namespace KamiyoModPack.Kamiyo_Re21341.MechUtil
{
    public class MechUtil_Kamiyo : MechUtilBase
    {
        public MechUtil_Kamiyo(MechUtilBaseModel model) : base(model)
        {
        }

        public override bool EgoActive()
        {
            if (!base.EgoActive()) return false;
            if (UsingSkinProjection()) ChangeDiceEffects(Model.Owner);
            return true;
        }

        public static void ChangeDiceEffects(BattleUnitModel owner)
        {
            foreach (var card in owner.allyCardDetail.GetAllDeck())
            {
                card.CopySelf();
                foreach (var dice in card.GetBehaviourList())
                    ChangeCardDiceEffect(dice);
            }
        }

        private static void ChangeCardDiceEffect(DiceBehaviour dice)
        {
            switch (dice.EffectRes)
            {
                case "KamiyoHit_Re21341":
                    dice.EffectRes = "KamiyoHitEgo_Re21341";
                    break;
                case "KamiyoSlash_Re21341":
                    dice.EffectRes = "KamiyoSlashEgo_Re21341";
                    break;
                case "PierceKamiyo_Re21341":
                    dice.EffectRes = "PierceKamiyoMask_Re21341";
                    break;
            }
        }

        public virtual bool UsingSkinProjection()
        {
            return !Model.EgoOptions.Any() || Model.EgoOptions.All(x => string.IsNullOrEmpty(x.Value.EgoSkinName));
        }
    }
}