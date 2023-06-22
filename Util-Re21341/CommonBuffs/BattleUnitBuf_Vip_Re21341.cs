using UtilLoader21341.Util;

namespace KamiyoModPack.Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_Vip_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_Vip_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "Vip_Re21341";
        protected override string keywordIconId => "Vip_Re21341";

        public override void OnDie()
        {
            _owner.VipDeath();
        }
    }
}