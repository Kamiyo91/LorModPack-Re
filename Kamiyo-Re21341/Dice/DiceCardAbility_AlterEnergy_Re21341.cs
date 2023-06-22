﻿using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Dice
{
    public class DiceCardAbility_AlterEnergyDie_Re21341 : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            if (owner.GetActiveBuff<BattleUnitBuf_AlterEgoRelease_Re21341>() == null &&
                !owner.ActivatedEmotionCard(KamiyoModParameters.PackageId, 21345)) return;
            behavior.card.target?.AddBuff<BattleUnitBuf_AlterEnergy_Re21341>(1, maxStack: 10);
        }
    }
}