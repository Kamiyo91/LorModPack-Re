namespace Angela_Re21341.Passives
{
    public class PassiveAbility_AngelaRegen_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            owner.allyCardDetail.DrawCards(2);
        }

        public override void OnRoundStart()
        {
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }

        public override void OnDrawCard()
        {
            owner.allyCardDetail.DrawCards(1);
        }
    }
}