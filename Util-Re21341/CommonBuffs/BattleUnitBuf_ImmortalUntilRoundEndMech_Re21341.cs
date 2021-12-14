namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmortalUntilRoundEndMech_Re21341 : BattleUnitBuf
    {
        public override bool IsImmortal() => true;

        public override bool IsInvincibleHp(BattleUnitModel attacker) => true;

        public override void OnRoundEnd() => Destroy();
        public override bool CanRecoverHp(int amount) => false;
    }
}
