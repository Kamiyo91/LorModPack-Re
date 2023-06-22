using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_EntertainMe_Re21341 : BattleUnitBuf
    {
        private int _addValue;
        public int MaxStack => 50;
        public override BufPositiveType positiveType => BufPositiveType.Positive;

        protected override string keywordId =>
            _owner.faction == Faction.Player ? "EntertainMePlayer_Re21341" : "EntertainMeNpc_Re21341";

        protected override string keywordIconId => "EntertainMe_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _addValue = 1;
        }

        public override void OnRoundEndTheLast()
        {
            OnAddBuf(_owner.faction == Faction.Enemy ? 3 : 1);
        }

        public override void OnAddBuf(int addedStack)
        {
            this.OnAddBufCustom(addedStack, maxStack: MaxStack);
        }

        public void SetValue(int value)
        {
            _addValue = value;
        }

        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            OnAddBuf(_addValue);
        }
    }
}