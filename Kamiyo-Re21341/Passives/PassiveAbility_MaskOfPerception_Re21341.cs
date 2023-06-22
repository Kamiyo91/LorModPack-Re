using LOR_DiceSystem;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Passives
{
    public class PassiveAbility_MaskOfPerception_Re21341 : PassiveAbilityBase
    {
        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            return buf.bufType != KeywordBuf.Paralysis && base.CanAddBuf(buf);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Evasion) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            if (attackerCard == null || owner.speedDiceResult.Count - 1 != attackerCard.targetSlotOrder) return;
            owner.SetPassiveCombatLog(this);
            attackerCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = -1,
                max = -1
            });
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            if (card == null || owner.speedDiceResult.Count - 1 != card.slotOrder) return;
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