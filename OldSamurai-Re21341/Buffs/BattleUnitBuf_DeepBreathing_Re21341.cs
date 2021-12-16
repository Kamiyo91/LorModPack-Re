using BLL_Re21341.Models;
using HarmonyLib;

namespace OldSamurai_Re21341.Buffs
{
    public class BattleUnitBuf_DeepBreathing_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_DeepBreathing_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "DeepBreathing_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all)
                ?.SetValue(this, ModParameters.ArtWorks["DeepBreathing_Re21341"]);
            typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all)?.SetValue(this, true);
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}