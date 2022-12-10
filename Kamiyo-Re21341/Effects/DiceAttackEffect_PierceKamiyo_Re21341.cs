using BigDLL4221.DiceEffects;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Kamiyo_Re21341.Effects
{
    public class DiceAttackEffect_PierceKamiyo_Re21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(KamiyoModParameters.Path, 0.725f, 0.185f, 2.5f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }

    public class DiceAttackEffect_PierceKamiyoMask_Re21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(KamiyoModParameters.Path, 0.725f, 0.185f, 2.5f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }
}