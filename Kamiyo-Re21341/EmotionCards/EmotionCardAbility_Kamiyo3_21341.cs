using System.Linq;
using KamiyoModPack.Kamiyo_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.EmotionCards
{
    public class EmotionCardAbility_Kamiyo3_21341 : EmotionCardAbilityBase
    {
        public override void OnRoundStart()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList().Where(x => x != _owner))
                unit.AddBuff<BattleUnitBuf_AlterEnergy_Re21341>(1, maxStack: 10);
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            var target = behavior.card?.target;
            if (target == null) return;
            _owner.SetEmotionCombatLog(_emotionCard);
            target.AddBuff<BattleUnitBuf_AlterEnergy_Re21341>(1);
            if (target.GetActiveBuff<BattleUnitBuf_AlterEnergy_Re21341>() != null) _owner.breakDetail.RecoverBreak(2);
        }
    }
}