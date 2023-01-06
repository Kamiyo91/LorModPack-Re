using KamiyoModPack.Hayate_Re21341.Passives;

namespace KamiyoModPack.Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_Ultima_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_Ultima_Re21341()
        {
            stack = 0;
        }

        protected override string keywordId => "UltimaBuff_Re21341";
        protected override string keywordIconId => "UltimaBuff_Re21341";
        public override int paramInBufDesc => 0;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.card.target != null &&
                behavior.card.target.passiveDetail.HasPassive<PassiveAbility_HighDivinity_Re21341>())
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        max = -1
                    });
        }
    }
}