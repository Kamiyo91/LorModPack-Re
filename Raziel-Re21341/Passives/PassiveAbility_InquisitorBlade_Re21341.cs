namespace Raziel_Re21341.Passives
{
    public class PassiveAbility_InquisitorBlade_Re21341 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                min = 1,
                max = 1
            });
        }
    }
}