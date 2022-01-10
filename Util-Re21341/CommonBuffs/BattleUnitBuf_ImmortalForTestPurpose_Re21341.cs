namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmortalForTestPurpose_Re21341 : BattleUnitBuf
    {
        public override bool IsImmortal()
        {
            return true;
        }

        public override bool IsInvincibleHp(BattleUnitModel attacker)
        {
            return true;
        }

        public override bool IsInvincibleBp(BattleUnitModel attacker)
        {
            return true;
        }

        public override void OnRoundStart()
        {
            _owner.cardSlotDetail.RecoverPlayPoint(_owner.cardSlotDetail.GetMaxPlayPoint());
        }

        //public override void BeforeRollDice(BattleDiceBehavior behavior)
        //{
        //    behavior.ApplyDiceStatBonus(
        //        new DiceStatBonus
        //        {
        //            min = 50,
        //            max = 50
        //        });
        //}
    }
}