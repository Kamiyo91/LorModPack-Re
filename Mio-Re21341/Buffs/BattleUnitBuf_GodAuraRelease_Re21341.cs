using Sound;
using UnityEngine;

namespace KamiyoModPack.Mio_Re21341.Buffs
{
    public class BattleUnitBuf_GodAuraRelease_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_GodAuraRelease_Re21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "GodAura_Re21341";
        protected override string keywordIconId => "Light_Re21341";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            var effect = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "5_T/FX_IllusionCard_5_T_Happiness", 1f, _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
            foreach (var particle in effect.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (!particle.gameObject.name.Contains("Force"))
                {
                    particle.gameObject.SetActive(false);
                    continue;
                }

                var main = particle.main;
                main.startColor = particle.gameObject.name.Equals("Force_burn")
                    ? new Color(1, 0, 0, 1)
                    : new Color(1, 1, 0.702f, 1);
            }
        }

        public override void OnRoundEnd()
        {
            RecoverHpAndStagger();
        }

        private void RecoverHpAndStagger()
        {
            _owner.RecoverHP(3);
            _owner.breakDetail.RecoverBreak(3);
        }
    }
}