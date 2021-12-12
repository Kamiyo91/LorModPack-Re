using Kamiyo_Re21341.Buffs;
using LOR_DiceSystem;
using Util_Re21341;

namespace Kamiyo_Re21341.Passives.Player
{
    public class PassiveAbility_MaskOfPerception_Re21341 : PassiveAbilityBase
    {
        private bool CheckEgo() => owner.bufListDetail.GetActivatedBufList()
            .Exists(x => x is BattleUnitBuf_AlterEgoRelease_Re21341);
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (!CheckEgo()) return;
            if (behavior.Detail != BehaviourDetail.Evasion) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            base.OnStartTargetedOneSide(attackerCard);
            if (!CheckEgo()) return;
            UnitUtil.SetPassiveCombatLog(this, owner);
            attackerCard?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            base.OnStartParrying(card);
            if (!CheckEgo()) return;
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
            UnitUtil.SetPassiveCombatLog(this, owner);
            battlePlayingCardDataInUnitModel2?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }
    }
}
