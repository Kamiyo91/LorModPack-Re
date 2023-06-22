using UtilLoader21341.Util;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_KurosawaBlade_Re21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            owner.SetPassiveCombatLog(this);
            owner.RecoverHP(2);
            owner.breakDetail.RecoverBreak(2);
        }
    }
}