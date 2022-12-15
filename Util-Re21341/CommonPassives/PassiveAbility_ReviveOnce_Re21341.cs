using BigDLL4221.Utils;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_ReviveOnce_Re21341 : PassiveAbilityBase
    {
        public bool Revived;

        public override void OnCreated()
        {
            Hide();
            Revived = false;
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead() || Revived) return;
            Revived = true;
            UnitUtil.UnitReviveAndRecovery(owner, owner.MaxHp, true);
        }
    }
}