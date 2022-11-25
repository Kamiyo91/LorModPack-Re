using BigDLL4221.Extensions;
using KamiyoModPack.Kamiyo_Re21341.Passives;

namespace KamiyoModPack.Kamiyo_Re21341.EmotionCards
{
    public class EmotionCardAbility_Kamiyo1_21341 : EmotionCardAbilityBase
    {
        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            _owner.SetEmotionCombatLog(_emotionCard);
            attackerCard?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
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
            _owner.SetEmotionCombatLog(_emotionCard);
            battlePlayingCardDataInUnitModel2?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }

        public override void OnRoundStart()
        {
            _owner.TakeDamage(2);
            _owner.TakeBreakDamage(2);
        }

        public override void OnSelectEmotion()
        {
            ActiveEgo();
        }

        public override void OnWaveStart()
        {
            ActiveEgo();
        }

        public void ActiveEgo()
        {
            var passive = _owner.GetActivePassive<PassiveAbility_AlterEgoPlayer_Re21341>();
            if (passive == null) return;
            if (!passive.Util.Model.EgoOptions.TryGetValue(passive.Util.Model.EgoPhase, out var egoOptions)) return;
            if (egoOptions.EgoActive) return;
            _owner.personalEgoDetail.RemoveCard(passive.Util.Model.FirstEgoFormCard);
            passive.Util.TurnEgoAbDialogOff();
            passive.Util.EgoActive();
        }
    }
}