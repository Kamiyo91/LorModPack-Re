using KamiyoModPack.BLL_Re21341.Models;
using UnityEngine;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_PossessorOfMioLight_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_PossessorOfMioLight_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "MioLight_Re21341";
        protected override string keywordIconId => "Light_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().FindAll(x =>
                         x.GetID() == new LorId(KamiyoModParameters.PackageId, 22) ||
                         x.GetID() == new LorId(KamiyoModParameters.PackageId, 19)))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }

            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            _owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            var aura = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "2_Y/FX_IllusionCard_2_Y_Charge", 1f, _owner.view, _owner.view);
            foreach (var particle in aura.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (particle.gameObject.name.Contains("Burn"))
                    particle.gameObject.AddComponent<AuraColorFragment>();
                if (particle.gameObject.name.Equals("Burn")) continue;
                particle.gameObject.SetActive(false);
            }
        }

        public override void OnRoundStartAfter()
        {
            _owner.cardSlotDetail.RecoverPlayPoint(1);
        }

        public override void OnRoundEnd()
        {
            RecoverHpAndStagger();
        }

        private void RecoverHpAndStagger()
        {
            _owner.RecoverHP(3);
            _owner.breakDetail.RecoverBreak(3);
        }
    }
}