using EmotionalBurstPassive_Re21341.Passives;

namespace EmotionalBurstPassive_Re21341.Buffs
{
    public class BattleUnitBuf_Angry_Re21341 : BattleUnitBuf
    {
        private BattleUnitModel _attacker;
        public override int paramInBufDesc => 0;
        protected override string keywordId =>
            stack == 1 ? "Angry_Re21341" : stack == 2 ? "Enraged_Re21341" : "Furious_Re21341";

        protected override string keywordIconId => "Aubrey_Re21341";

        public override void BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _attacker = attacker;
            base.BeforeTakeDamage(attacker, dmg);
        }

        public override int GetDamageReductionRate()
        {
            if (_attacker != null && _attacker.passiveDetail.HasPassive<PassiveAbility_Sad_Re21341>())
                return 10 * stack;
            return base.GetDamageReductionRate();
        }

        public override int GetBreakDamageReductionRate()
        {
            if (_attacker != null && _attacker.passiveDetail.HasPassive<PassiveAbility_Sad_Re21341>())
                return 10 * stack;
            return base.GetBreakDamageReductionRate();
        }

        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            if (!behavior.card.target.passiveDetail.HasPassive<PassiveAbility_Sad_Re21341>()) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmgRate = 10 * stack,
                breakRate = 10 * stack
            });
        }
    }
}