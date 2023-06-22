using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Passives
{
    public class PassiveAbility_KamiyoFakeShimmering_Re21341 : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override void OnRoundStartAfter()
        {
            owner.DrawUntilX(9);
            owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
        }
    }
}