using Util_Re21341;

namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_FireOverflow_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            SkinUtil.BurnEffect(unit);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 3, unit);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, unit);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, unit);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }
    }
}