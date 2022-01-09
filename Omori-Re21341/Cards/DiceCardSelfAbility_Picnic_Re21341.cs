﻿using System.Linq;
using EmotionalBurstPassive_Re21341.Passives;

namespace Omori_Re21341.Cards
{
    public class DiceCardSelfAbility_Picnic_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (BattleObjectManager.instance.GetAliveList(Faction.Player).Count < 2 || !BattleObjectManager.instance.GetAliveList(Faction.Player).All(x =>
                    x.passiveDetail.PassiveList.Exists(y => !y.destroyed && y is PassiveAbility_Happy_Re21341))) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player).Where(x => x != owner))
            {
                unit.allyCardDetail.DrawCards(1);
                unit.cardSlotDetail.RecoverPlayPoint(1);
            }
        }
    }
}