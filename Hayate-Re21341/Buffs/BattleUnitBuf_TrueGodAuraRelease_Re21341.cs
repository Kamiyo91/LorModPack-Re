using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using HarmonyLib;
using Hayate_Re21341.Buffs;
using Sound;
using Util_Re21341;

namespace Mio_Re21341.Buffs
{
    public class BattleUnitBuf_TrueGodAuraRelease_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_TrueGodAuraRelease_Re21341() => stack = 0;
        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "TrueGodAura_Re21341";
        public override void BeforeRollDice(BattleDiceBehavior behavior) =>
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 2
                });

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all)
                ?.SetValue(this, ModParameters.ArtWorks["TrueGodAura_Re21341"]);
            typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all)?.SetValue(this, true);
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
