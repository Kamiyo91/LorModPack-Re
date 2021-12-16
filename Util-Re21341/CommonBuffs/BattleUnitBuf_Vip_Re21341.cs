using BLL_Re21341.Models;
using HarmonyLib;

namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_Vip_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_Vip_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "Vip_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all)
                ?.SetValue(this, ModParameters.ArtWorks["Vip_Re21341"]);
            typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all)?.SetValue(this, true);
        }

        public override void OnDie()
        {
            UnitUtil.VipDeath(_owner);
        }
    }
}