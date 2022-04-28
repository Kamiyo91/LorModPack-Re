using KamiyoStaticUtil.Utils;
using Sound;

namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_WillOWispAura_Re21341 : BattleUnitBuf
    {
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
            UnitUtil.MakeEffect(_owner, "6/BigBadWolf_Emotion_Aura", 1f, _owner);
        }
    }
}