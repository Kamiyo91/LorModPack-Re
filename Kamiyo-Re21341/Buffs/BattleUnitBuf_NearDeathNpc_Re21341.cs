using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_NearDeathNpc_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_NearDeathNpc_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "SuddenDeathNpc_Re21341";
        protected override string keywordIconId => "SuddenDeath_Re21341";

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            behavior.card.target?.AddBuffCustom<BattleUnitBuf_AlterEnergy_Re21341>(1, maxStack: 10);
        }

        public override bool CanRecoverHp(int amount)
        {
            if (_owner.hp + amount < 161) return true;
            _owner.SetHp(161);
            return false;
        }
    }
}