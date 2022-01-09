﻿using System.Linq;
using EmotionalBurstPassive_Re21341;

namespace Omori_Re21341.Cards
{
    public class DiceCardSelfAbility_Friends_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (BattleObjectManager.instance.GetAliveList(Faction.Player).Count < 2 || !BattleObjectManager.instance.GetAliveList(Faction.Player)
                    .All(EmotionalBurstUtil.CheckEmotionPassives)) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }

            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, owner);
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, owner);
        }
    }
}