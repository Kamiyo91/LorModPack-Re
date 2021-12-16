namespace EmotionalBurstPassive_Re21341.Passives
{
    public class PassiveAbility_EmotionalBurst_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            PrepareEmotionalBurstCards();
        }

        private void PrepareEmotionalBurstCards()
        {
            EmotionalBurstUtil.RemoveEmotionalBurstCards(owner);
            EmotionalBurstUtil.AddEmotionalBurstCards(owner);
        }
    }
}