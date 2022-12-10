using BigDLL4221.DiceEffects;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Kamiyo_Re21341.Effects
{
    public class DiceAttackEffect_KamiyoSlash_Re21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(KamiyoModParameters.Path, 0.55f, 0.15f, 2f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }

    public class DiceAttackEffect_KamiyoSlashEgo_Re21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(KamiyoModParameters.Path, 0.55f, 0.15f, 2f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }
}