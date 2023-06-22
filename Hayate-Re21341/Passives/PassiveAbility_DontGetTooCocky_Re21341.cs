using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Extensions;
using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_DontGetTooCocky_Re21341 : PassiveAbilityBase
    {
        private bool _used;

        public override void OnDrawParrying(BattleDiceBehavior behavior)
        {
            if (behavior.TargetDice.DiceResultValue < 50 || _used) return;
            _used = true;
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 907));
            owner.UnitReviveAndRecovery(0, false);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_DLL21341(isImmortalBp: true));
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (behavior.TargetDice.DiceResultValue < 50 || _used) return;
            _used = true;
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 907));
            owner.UnitReviveAndRecovery(0, false);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_DLL21341(isImmortalBp: true));
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (atkDice.DiceResultValue < 50 || _used) return;
            _used = true;
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 907));
            owner.UnitReviveAndRecovery(0, false);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_DLL21341(isImmortalBp: true));
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _used = false;
        }
    }
}