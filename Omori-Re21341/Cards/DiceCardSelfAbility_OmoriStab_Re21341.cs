using EmotionalBurstPassive_Re21341.Buffs;
using EmotionalBurstPassive_Re21341.Passives;

namespace Omori_Re21341.Cards
{
    public class DiceCardSelfAbility_OmoriStab_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (card.target != null && card.target.passiveDetail.HasPassive<PassiveAbility_Happy_Re21341>())
            {
                owner.allyCardDetail.DrawCards(1);
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    dmgRate = 25
                });
            }

            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Sad_Re21341) is
                    BattleUnitBuf_Sad_Re21341 buf && buf.BufValue > 2)
                foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                             .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
                {
                    battleDiceCardModel.GetBufList();
                    battleDiceCardModel.AddCost(-1);
                }

            if (owner.passiveDetail.HasPassive<PassiveAbility_Sad_Re21341>())
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    dmgRate = 25
                });
        }
    }
}