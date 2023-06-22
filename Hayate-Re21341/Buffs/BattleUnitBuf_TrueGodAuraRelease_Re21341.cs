using Sound;

namespace KamiyoModPack.Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_TrueGodAuraRelease_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_TrueGodAuraRelease_Re21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "TrueGodAuraRelease_Re21341";
        protected override string keywordIconId => "TrueGodAura_Re21341";

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
            var buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_EntertainMe_Re21341) as
                BattleUnitBuf_EntertainMe_Re21341;
            buf?.SetValue(2);
        }

        private void InitAuraAndPlaySound()
        {
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
            var aura = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect("6/BigBadWolf_Emotion_Aura",
                1f, _owner.view,
                _owner.view);
            aura.gameObject.AddComponent<AuraColor>();
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }
    }
}