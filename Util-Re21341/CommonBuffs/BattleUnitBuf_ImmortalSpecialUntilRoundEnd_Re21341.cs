namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmortalSpecialUntilRoundEnd_Re21341 : BattleUnitBuf
    {
        public override bool IsImmortal()
        {
            return true;
        }

        public override bool IsInvincibleBp(BattleUnitModel attacker)
        {
            return true;
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}