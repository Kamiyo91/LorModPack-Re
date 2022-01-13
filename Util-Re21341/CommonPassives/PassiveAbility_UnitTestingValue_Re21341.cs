namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_UnitTestingValue_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            switch (id.id)
            {
                case 58:
                    UnitUtil.TestingUnitValuesImmortality();
                    break;
                case 60:
                    UnitUtil.TestingUnitValuesBigDamage();
                    break;
            }
        }
    }
}