using BigDLL4221.Extensions;
using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_ImminentCombat_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 1;
        private int _defClashWin;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _defClashWin = 0;
            var buff = owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>();
            if (buff == null)
            {
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Shock_Re21341());
                return;
            }

            if (buff.stack < 3) return;
            buff.OnAddBuf(-3);
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus { power = 1 });
        }

        public override void OnWinParryingDef()
        {
            _defClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_defClashWin < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
        }
    }
}