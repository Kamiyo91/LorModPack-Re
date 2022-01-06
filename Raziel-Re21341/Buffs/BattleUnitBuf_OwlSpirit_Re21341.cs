using Sound;
using UnityEngine;

namespace Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_OwlSpirit_Re21341 : BattleUnitBuf
    {
        private static readonly int Power = RandomUtil.Range(0, 2);
        private GameObject _aura;

        public BattleUnitBuf_OwlSpirit_Re21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "Kaioken_Re21341";
        protected override string keywordIconId => "Kaioken_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            var effect =
                SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                    "8_B/FX_IllusionCard_8_B_Punising", 1f, _owner.view, _owner.view);
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