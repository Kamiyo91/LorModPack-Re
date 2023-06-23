using KamiyoModPack.Mio_Re21341.Passives;
using Sound;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Buffs
{
    public class BattleUnitBuf_CorruptedGodAuraRelease_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_CorruptedGodAuraRelease_Re21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "CorruptedGodAura_Re21341";
        protected override string keywordIconId => "Final_BigBird_Darkness";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            var target = behavior.card?.target;
            if (target != null && target.passiveDetail.HasPassive<PassiveAbility_GodFragment_Re21341>()) return;
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 3
                });
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
            ParticleEffectsUtil.MakeEffect(_owner, "6/BigBadWolf_Emotion_Aura", 1f, _owner);
            SoundEffectPlayer.PlaySound("Creature/Angry_Meet");
        }

        public override void OnRoundEnd()
        {
            TakeDamageByEffect();
        }

        private void TakeDamageByEffect()
        {
            _owner.TakeDamage(45, DamageType.Emotion);
            _owner.breakDetail.TakeBreakDamage(45, DamageType.Emotion);
        }
    }
}