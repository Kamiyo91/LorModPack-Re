namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmortalForTestPurpose_Re21341 : BattleUnitBuf
    {
        public override bool IsImmortal() => true;

        public override bool IsInvincibleHp(BattleUnitModel attacker) => true;

        public override bool IsInvincibleBp(BattleUnitModel attacker) => true;

        public override void OnRoundStart() => _owner.cardSlotDetail.RecoverPlayPoint(_owner.cardSlotDetail.GetMaxPlayPoint());
    }
}
