namespace EmotionalBurstPassive_Re21341.Passives
{
    public class PassiveAbility_Neutral_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}