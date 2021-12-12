using BLL_Re21341.Models;
using UnityEngine;

namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_OutragingFire_Re21341 : DiceCardSelfAbilityBase
    {
        private int _atkClashWin;
        private const int Check = 2;
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkClashWin = 0;
        }

        public override void OnWinParryingAtk() => _atkClashWin++;

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction == Faction.Player ? Faction.Enemy : Faction.Player))
                unit.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn,1);
        }
    }
}
