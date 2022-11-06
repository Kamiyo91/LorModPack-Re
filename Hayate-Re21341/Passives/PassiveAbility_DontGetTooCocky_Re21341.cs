using BigDLL4221.Buffs;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

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
            UnitUtil.UnitReviveAndRecovery(owner, 0, false);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_DLL4221(isImmortalBp: true));
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (behavior.TargetDice.DiceResultValue < 50 || _used) return;
            _used = true;
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 907));
            UnitUtil.UnitReviveAndRecovery(owner, 0, false);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_DLL4221(isImmortalBp: true));
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (atkDice.DiceResultValue < 50 || _used) return;
            _used = true;
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 907));
            UnitUtil.UnitReviveAndRecovery(owner, 0, false);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_DLL4221(isImmortalBp: true));
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _used = false;
        }
    }
}