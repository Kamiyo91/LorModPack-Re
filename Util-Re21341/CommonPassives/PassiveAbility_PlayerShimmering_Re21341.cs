namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_PlayerShimmering_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            if(UnitUtil.CheckCardCost(owner,0))UnitUtil.ChangeCardCostByValue(owner, -99, 99);
            UnitUtil.DrawUntilX(owner, 6);
        }
    }
}