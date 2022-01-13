namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_BigDamageForTestingPurpose_Re21341 : BattleUnitBuf
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    min = 50,
                    max = 50
                });
        }
    }
}