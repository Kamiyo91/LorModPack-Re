using BigDLL4221.Utils;
using Sound;

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
            ArtUtil.MakeEffect(_owner, "6/BigBadWolf_Emotion_Aura", 1f, _owner);
        }

        public override void OnRoundEnd()
        {
            TakeDamageByEffect();
        }

        private void TakeDamageByEffect()
        {
            _owner.TakeDamage(20, DamageType.Emotion);
            _owner.breakDetail.TakeBreakDamage(20, DamageType.Emotion);
        }
    }
}