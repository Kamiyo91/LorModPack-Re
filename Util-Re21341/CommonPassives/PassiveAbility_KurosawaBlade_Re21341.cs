using LOR_DiceSystem;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_KurosawaBlade_Re21341 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Slash) return;
            UnitUtil.SetPassiveCombatLog(this, owner);
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            owner.RecoverHP(2);
            owner.breakDetail.RecoverBreak(2);
        }
    }
}
