﻿using UtilLoader21341.Extensions;

namespace KamiyoModPack.Util_Re21341.CommonCards
{
    public class DiceCardSelfAbility_CustomInstantIndexRelease_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_CustomInstantIndexRelease_DLL21341());
        }
    }
}