using BLL_Re21341.Models;
using Sound;

namespace Kamiyo_Re21341.Buffs
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
            SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "5_T/FX_IllusionCard_5_T_Happiness", 1f, _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
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