using BigDLL4221.DiceEffects;
using KamiyoModPack.BLL_Re21341.Models;
using UnityEngine;

namespace KamiyoModPack.Mio_Re21341.Effects
{
    public class DiceAttackEffect_MioPierce_Re21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(KamiyoModParameters.Path, 0.725f, 0.315f, 2.5f);
            base.Initialize(self, target, destroyTime);
            var portal = Util.LoadPrefab("Battle/CreatureEffect/5/KingOfGreed_Portal", transform);
            portal.transform.position = self.WorldPosition + new Vector3(1 * 0f, 0f, 0f);
        }
    }
}