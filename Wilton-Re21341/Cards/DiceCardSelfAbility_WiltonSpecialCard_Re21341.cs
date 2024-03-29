﻿using System.Linq;
using KamiyoModPack.Wilton_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_WiltonSpecialCard_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_Vengeance_Re21341)
                ?.stack > 24;
        }

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit, targetUnit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit, BattleUnitModel targetUnit)
        {
            if (!(unit.bufListDetail.GetActivatedBufList()
                        .FirstOrDefault(x => x is BattleUnitBuf_Vengeance_Re21341) is BattleUnitBuf_Vengeance_Re21341
                    buff)) return;
            targetUnit.TakeDamage(buff.stack);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, unit);
            unit.cardSlotDetail.RecoverPlayPoint(1);
            targetUnit.bufListDetail.RemoveBufAll(BufPositiveType.Positive);
            targetUnit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Weak, 1, unit);
            targetUnit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Disarm, 1, unit);
            ParticleEffectsUtil.BaseGameLoadPrefabEffect(targetUnit,
                "Battle/DiceAttackEffects/New/FX/DamageDebuff/FX_DamageDebuff_Blooding", "Buf/Effect_Bleeding");
            unit.bufListDetail.AddBuf(new BattleUnitBuff_LowerCostTo0UntilRoundEnd_Re21341());
        }
    }
}