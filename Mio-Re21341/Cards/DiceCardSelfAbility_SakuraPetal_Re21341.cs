﻿using KamiyoModPack.Mio_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_SakuraPetal_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            if (!(unit.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_SakuraPetal_Re21341) is
                    BattleUnitBuf_SakuraPetal_Re21341 buf)) return;
            ParticleEffectsUtil.IndexReleaseBreakEffect(unit);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, unit);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, unit);
            buf.Destroy();
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }
    }
}