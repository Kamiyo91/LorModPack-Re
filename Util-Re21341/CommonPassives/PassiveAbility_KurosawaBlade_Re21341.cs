using KamiyoStaticUtil.Utils;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_KurosawaBlade_Re21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            UnitUtil.SetPassiveCombatLog(this, owner);
            owner.RecoverHP(2);
            owner.breakDetail.RecoverBreak(2);
        }
    }
}