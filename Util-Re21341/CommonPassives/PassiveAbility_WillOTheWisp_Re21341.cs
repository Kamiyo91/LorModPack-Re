using System.Linq;
using KamiyoModPack.BLL_Re21341.Models;
using Sound;
using UtilLoader21341.Util;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_WillOTheWisp_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.Book.BookId != new LorId(KamiyoModParameters.PackageId, 9)) return;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_WolfBlueAura_Re21341());
        }

        public override void OnRoundEnd()
        {
            if (BattleObjectManager.instance.GetAliveList(owner.faction)
                    .Count(x => x.passiveDetail.HasPassive<PassiveAbility_WillOTheWisp_Re21341>()) <= 2) return;
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, owner);
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, owner);
        }
    }

    public class BattleUnitBuf_WolfBlueAura_Re21341 : BattleUnitBuf
    {
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
            ParticleEffectsUtil.MakeEffect(_owner, "6/BigBadWolf_Emotion_Aura", 1f, _owner);
        }
    }
}