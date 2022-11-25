using BigDLL4221.Buffs;
using UnityEngine;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_Shock_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        public BattleUnitBuf_Shock_Re21341() : base(infinite: true, lastOneScene: false)
        {
        }

        protected override string keywordId => "Shock_Re21341";
        protected override string keywordIconId => "Shock_Re21341";
        public override int MaxStack => 10;

        public override void OnRoundStart()
        {
            if (stack > 0 && !_owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>())
                _owner.TakeBreakDamage(stack);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (_owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>()) return;
            var debuffValue = Mathf.Clamp(stack / 3, 0, 3);
            if (debuffValue == 0) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { min = -debuffValue });
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            OnAddBuf(1);
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (_owner.bufListDetail.HasBuf<BattleUnitBuf_NearDeath_Re21341>() ||
                _owner.bufListDetail.HasBuf<BattleUnitBuf_NearDeathNpc_Re21341>()) return;
            OnAddBuf(-1);
        }
    }
}