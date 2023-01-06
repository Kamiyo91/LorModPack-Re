using BigDLL4221.Buffs;

namespace KamiyoModPack.Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_EntertainMe_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        private int _addValue;

        public BattleUnitBuf_EntertainMe_Re21341() : base(infinite: true, lastOneScene: false)
        {
        }

        public override int MinStack => -50;
        public override int MaxStack => 50;
        public override BufPositiveType positiveType => BufPositiveType.Positive;

        protected override string keywordId =>
            _owner.faction == Faction.Player ? "EntertainMePlayer_Re21341" : "EntertainMeNpc_Re21341";

        protected override string keywordIconId => "EntertainMe_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _addValue = 1;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    min = stack < 0 ? -1 : 1
                });
        }

        public override void OnRoundEndTheLast()
        {
            OnAddBuf(_owner.faction == Faction.Enemy ? 3 : 1);
        }


        public void SetValue(int value)
        {
            _addValue = value;
        }

        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            OnAddBuf(_addValue);
        }

        public override void BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (attacker == null) return;
            OnAddBuf(-_addValue);
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return stack < 0 ? -1 : base.GetCardCostAdder(card);
        }
    }
}