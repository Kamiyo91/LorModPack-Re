using BigDLL4221.Utils;

namespace KamiyoModPack.OldSamurai_Re21341.Passives
{
    public class PassiveAbility_DeepBreathing_Re21341 : PassiveAbilityBase
    {
        public override void Init(BattleUnitModel self)
        {
            Hide();
            base.Init(self);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            UnitUtil.SetPassiveCombatLog(this, owner);
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    max = 3
                });
        }
    }
}