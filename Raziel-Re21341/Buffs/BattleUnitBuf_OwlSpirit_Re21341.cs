using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sound;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_OwlSpirit_Re21341 : BattleUnitBuf
    {
        private static int Power = RandomUtil.Range(0, 2);
        private GameObject _aura;
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            var effect = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect("8_B/FX_IllusionCard_8_B_Punising", 1f, _owner.view, _owner.view);
            _aura = effect != null ? effect.gameObject : null;
            SoundEffectPlayer.PlaySound("Creature/SmallBird_StrongAtk");
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            var pow = Power;
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                min = pow,
                max = pow
            });
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }

        public override void OnDie()
        {
            Destroy();
        }

        public override void Destroy()
        {
            DestroyAura();
            base.Destroy();
        }

        private void DestroyAura()
        {
            if (_aura == null) return;
            Object.Destroy(_aura);
            _aura = null;
        }
    }
}
