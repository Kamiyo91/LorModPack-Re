namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmortalUntilRoundEnd_Re21341 : BattleUnitBuf
    {
        public override bool IsImmortal() => true;

        public override bool IsInvincibleHp(BattleUnitModel attacker) => true;

        public override void OnRoundEnd() => Destroy();
    }
}
