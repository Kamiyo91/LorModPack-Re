﻿using BigDLL4221.DiceEffects;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Mio_Re21341.Effects
{
    public class DiceAttackEffect_MioSlash_Re21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(KamiyoModParameters.Path, 0.7f, 0.275f, 2.5f);
            base.Initialize(self, target, destroyTime);
        }
    }
}