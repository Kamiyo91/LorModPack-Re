using BigDLL4221.Extensions;
using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_WideSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private int _atkClashWin;
        private int _stacks;
        public override string[] Keywords => new[] { "ShockKeyword_Re21341" };

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkClashWin = 0;
            var buff = owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>();
            if (buff == null || buff.stack < 10)
            {
                if (buff == null) owner.bufListDetail.AddBuf(new BattleUnitBuf_Shock_Re21341());
                _stacks = 1;
                return;
            }

            buff.OnAddBuf(-10);
            _stacks = 2;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, _stacks, owner);
            var buff = card.target?.GetActiveBuff<BattleUnitBuf_AlterEnergy_Re21341>();
            if (buff == null || buff.stack < 10) return;
            buff.OnEndBattle(null);
            buff.OnAddBuf(-10);
            owner.RecoverHP(5);
        }
    }
}