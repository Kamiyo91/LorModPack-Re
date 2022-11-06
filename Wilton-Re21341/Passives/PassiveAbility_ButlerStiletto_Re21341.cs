namespace KamiyoModPack.Wilton_Re21341.Passives
{
    public class PassiveAbility_ButlerStiletto_Re21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.card.target.bufListDetail.GetActivatedBufList().Exists(x => x.bufType == KeywordBuf.Bleeding))
                owner.RecoverHP(2);
            if (behavior.card.target.bufListDetail.GetActivatedBufList()
                .Exists(x => x.bufType == KeywordBuf.Vulnerable)) owner.breakDetail.RecoverBreak(2);
        }
    }
}