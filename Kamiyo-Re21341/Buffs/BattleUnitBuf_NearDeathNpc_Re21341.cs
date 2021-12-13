using BLL_Re21341.Models;
using HarmonyLib;

namespace Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_NearDeathNpc_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_NearDeathNpc_Re21341() => stack = 0;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "SuddenDeathNpc_Re21341";
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all)
                ?.SetValue(this, ModParameters.ArtWorks["SuddenDeath_Re21341"]);
            typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all)?.SetValue(this, true);
        }
        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(_owner.faction == Faction.Player
                        ? Faction.Enemy
                        : Faction.Player))
            {
                unit.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 1, unit);
            }
        }

        public override bool CanRecoverHp(int amount)
        {
            if (_owner.hp + amount < 161) return true;
            _owner.SetHp(161);
            return false;

        }
    }
}
