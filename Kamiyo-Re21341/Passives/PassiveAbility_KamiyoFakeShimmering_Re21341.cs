using BigDLL4221.Utils;

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
            UnitUtil.DrawUntilX(owner, 9);
            owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
        }
    }
}