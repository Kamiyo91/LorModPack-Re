using Sound;
using Util_Re21341;

namespace Hayate_Re21341.Buffs
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
        protected override string keywordIconId => "TrueGodAuraRelease_Re21341";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 2
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
            UnitUtil.MakeEffect(_owner, "6/BigBadWolf_Emotion_Aura", 1f, _owner);
        }
    }
}