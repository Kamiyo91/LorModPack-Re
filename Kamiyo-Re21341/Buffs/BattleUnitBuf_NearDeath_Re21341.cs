namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_NearDeath_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_NearDeath_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "SuddenDeath_Re21341";
        protected override string keywordIconId => "SuddenDeath_Re21341";

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(_owner.faction == Faction.Player
                         ? Faction.Enemy
                         : Faction.Player))
                unit.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, unit);
        }

        public override bool CanRecoverHp(int amount)
        {
            if (_owner.hp + amount < 64) return true;
            _owner.SetHp(64);
            return false;
        }
    }
}