using System.Linq;
using OldSamurai_Re21341.Buffs;
using Util_Re21341;

namespace OldSamurai_Re21341.Passives
{
    public class PassiveAbility_OldSamuraiEnemyDesc_Re21341 : PassiveAbilityBase
    {
        public override void OnDie()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_OldSamuraiEgoNpc_Re21341>()) return;
            UnitUtil.VipDeath(owner);
        }
    }
}
