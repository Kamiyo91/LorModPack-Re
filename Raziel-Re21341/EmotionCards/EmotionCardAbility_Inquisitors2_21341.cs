using KamiyoModPack.Raziel_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Raziel_Re21341.EmotionCards
{
    public class EmotionCardAbility_Inquisitors2_21341 : EmotionCardAbilityBase
    {
        private bool _buffGiven;

        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            if (attackerCard?.owner.GetActiveBuff<BattleUnitBuf_Judgment_Re21341>() == null) return;
            _owner.SetEmotionCombatLog(_emotionCard);
            attackerCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = -1
            });
        }

        public override void OnParryingStart(BattlePlayingCardDataInUnitModel card)
        {
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel;
            if (card == null)
            {
                battlePlayingCardDataInUnitModel = null;
            }
            else
            {
                var target = card.target;
                battlePlayingCardDataInUnitModel = target?.currentDiceAction;
            }

            var battlePlayingCardDataInUnitModel2 = battlePlayingCardDataInUnitModel;
            if (battlePlayingCardDataInUnitModel2?.owner.GetActiveBuff<BattleUnitBuf_Judgment_Re21341>() ==
                null) return;
            _owner.SetEmotionCombatLog(_emotionCard);
            battlePlayingCardDataInUnitModel2.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = -1
            });
        }

        public override void OnRoundStart()
        {
            _buffGiven = false;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (_buffGiven || behavior.card?.target == null) return;
            _buffGiven = true;
            behavior.card?.target.bufListDetail.AddBuf(new BattleUnitBuf_Judgment_Re21341());
        }
    }
}