using BigDLL4221.Passives;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Raziel_Re21341.Passives
{
    public class PassiveAbility_Inquisitor_Re21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = 1,
                dmgRate = 25
            });
        }

        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new RazielUtil().RazielPlayerUtil);
        }

        public override void OnBattleEnd()
        {
            base.OnBattleEnd();
            UnitUtil.UnitReviveAndRecovery(owner, owner.MaxHp, false);
        }
    }
}