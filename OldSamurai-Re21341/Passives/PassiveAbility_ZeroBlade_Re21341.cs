using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.OldSamurai_Re21341.Dice;
using UtilLoader21341.Util;

namespace KamiyoModPack.OldSamurai_Re21341.Passives
{
    public class PassiveAbility_ZeroBlade_Re21341 : PassiveAbilityBase
    {
        private bool _counterReload;

        public override void OnStartBattle()
        {
            owner.ReadyCounterCard(2, KamiyoModParameters.PackageId);
            CardUtil.PrepareCounterDieOrderGameObject(owner, typeof(DiceCardAbility_ZeroBlade_Re21341), false);
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
            owner.SetPassiveCombatLog(this);
            owner.ReadyCounterCard(2, KamiyoModParameters.PackageId);
        }
    }
}