namespace Omori_Re21341.Passives
{
    public class PassiveAbility_KitchenKnife_Re21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, 1, owner);
        }
    }
}