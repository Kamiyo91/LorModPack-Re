using Roland_Re21341.Buffs;

namespace Roland_Re21341.Cards
{
    public class DiceCardSelfAbility_BlackSilenceEgoMaskScream_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.HasBuf<BattleUnitBuf_BlackSilenceEgoMask_Re21341>();
        }

        public override void OnUseCard()
        {
            owner.view.SetAltSkin("BlackSilence4");
        }

        public override void OnEndBattle()
        {
            owner.view.SetAltSkin("BlackSilence3");
        }
    }
}