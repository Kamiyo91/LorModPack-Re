using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_FireAbsorb_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>();
        }

        public override void OnStartBattle()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 3, owner);
            var positiveNum = owner.bufListDetail.GetKewordBufStack(KeywordBuf.Burn);
            if (positiveNum > 0)
                positiveNum /= 3;
            owner.bufListDetail.RemoveBufAll(KeywordBuf.Burn);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, positiveNum, owner);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, positiveNum, owner);
        }
    }
}