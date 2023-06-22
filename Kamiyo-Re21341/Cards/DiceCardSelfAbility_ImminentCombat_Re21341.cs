using KamiyoModPack.Kamiyo_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_ImminentCombat_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 1;
        private int _defClashWin;
        public override string[] Keywords => new[] { "ShockKeyword_Re21341" };

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _defClashWin = 0;
            var buff = owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>();
            if (buff == null || buff.stack < 25) return;
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus { power = 1 });
        }

        public override void OnWinParryingDef()
        {
            _defClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_defClashWin < Check) return;
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Protection, 2, owner);
        }
    }
}