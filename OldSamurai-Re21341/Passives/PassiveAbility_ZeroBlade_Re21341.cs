using OldSamurai_Re21341.Dice;
using Util_Re21341;

namespace OldSamurai_Re21341.Passives
{
    public class PassiveAbility_ZeroBlade_Re21341 : PassiveAbilityBase
    {
        private bool _counterReload;

        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 2);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.abilityList.Exists(x => x is DiceCardAbility_ZeroBlade_Re21341) &&
                owner.passiveDetail.PassiveList.Exists(x =>
                    x is PassiveAbility_OldSamurai_Re21341 || x is PassiveAbility_OldSamuraiEnemyDesc_Re21341))
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        min = 2, max = 2
                    });
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            _counterReload = false;
        }

        public override void OnDrawParrying(BattleDiceBehavior behavior)
        {
            _counterReload = false;
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            _counterReload = behavior.abilityList.Exists(x => x is DiceCardAbility_ZeroBlade_Re21341);
        }

        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            if (!_counterReload) return;
            _counterReload = false;
            UnitUtil.SetPassiveCombatLog(this, owner);
            UnitUtil.ReadyCounterCard(owner, 2);
        }
    }
}