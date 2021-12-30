namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_PlayerShimmering_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            UnitUtil.ChangeCardCostByValue(owner, -99, 99);
        }

        public override void OnRoundStartAfter()
        {
            UnitUtil.DrawUntilX(owner, 6);
        }
    }
}