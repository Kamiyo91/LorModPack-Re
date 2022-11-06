namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_HighDivinity_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}