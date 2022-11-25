using BigDLL4221.Extensions;
using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_WideSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private int _atkClashWin;
        private int _stacks;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkClashWin = 0;
            var buff = owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>();
            if (buff == null || buff.stack < 5)
            {
                _stacks = 2;
                return;
            }

            buff.OnAddBuf(-5);
            _stacks = 5;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction == Faction.Player
                         ? Faction.Enemy
                         : Faction.Player))
                unit.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, _stacks);
        }
    }
}