using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Passives
{
    public class PassiveAbility_MaskOfPerceptionNpc_Re21341 : PassiveAbilityBase
    {
        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            return buf.bufType != KeywordBuf.Paralysis && base.CanAddBuf(buf);
        }

        public override bool IsTargetable_theLast()
        {
            return false;
        }

        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            owner.SetPassiveCombatLog(this);
            attackerCard?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = -1,
                max = -1
            });
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            if (card == null) return;
            var target = card.target;
            var attackerCard = target?.currentDiceAction;
            owner.SetPassiveCombatLog(this);
            attackerCard?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = -1,
                max = -1
            });
        }
    }
}