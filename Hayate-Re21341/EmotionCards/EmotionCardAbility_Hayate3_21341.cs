using System;
using BigDLL4221.Extensions;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Hayate_Re21341.EmotionCards
{
    public class EmotionCardAbility_Hayate3_21341 : EmotionCardAbilityBase
    {
        private readonly Random _random = new Random();
        private bool _counterReload;
        private bool _soloCheck;

        public override void OnStartBattle()
        {
            if (UnitUtil.SupportCharCheck(_owner) == 1)
            {
                _soloCheck = true;
                UnitUtil.ReadyCounterCard(_owner, _random.Next(31, 35), KamiyoModParameters.PackageId);
            }
            else
            {
                _soloCheck = false;
            }
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (!_counterReload && _soloCheck)
                _counterReload = behavior.abilityList.Exists(x => x is DiceCardAbility_HayateCounter_Re21341);
        }

        public override void OnDrawParrying(BattleDiceBehavior behavior)
        {
            if (!_counterReload && _soloCheck)
                _counterReload = behavior.abilityList.Exists(x => x is DiceCardAbility_HayateCounter_Re21341);
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            if (!_counterReload && _soloCheck)
                _counterReload = behavior.abilityList.Exists(x => x is DiceCardAbility_HayateCounter_Re21341);
        }

        public override void OnEndParrying(BattlePlayingCardDataInUnitModel curCard)
        {
            if (!_counterReload) return;
            _counterReload = false;
            _owner.SetEmotionCombatLog(_emotionCard);
            UnitUtil.ReadyCounterCard(_owner, _random.Next(31, 35), KamiyoModParameters.PackageId);
        }
    }
}