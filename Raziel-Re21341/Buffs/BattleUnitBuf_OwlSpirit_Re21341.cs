using Sound;
using UnityEngine;

namespace KamiyoModPack.Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_OwlSpirit_Re21341 : BattleUnitBuf
    {
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
            if (_aura == null) return;
            foreach (var particle in _aura.gameObject.GetComponentsInChildren<ParticleSystem>())
                if (particle.gameObject.name.Contains("Bird"))
                    particle.gameObject.SetActive(false);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            var pow = RandomUtil.Range(0, 2);
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                min = pow,
                max = pow
            });
        }
    }
}