using EmotionalBurstPassive_Re21341.Passives;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_Comeback_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.allyCardDetail.DrawCards(1);
            if (owner.passiveDetail.HasPassive<PassiveAbility_Sad_Re21341>())
            {
                owner.RecoverHP(20);
                owner.breakDetail.RecoverBreak(20);
                owner.cardSlotDetail.RecoverPlayPoint(2);
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, owner);
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, owner);
            }

            var tempCard = new DiceCardSelfAbility_Happy_Re21341();
            tempCard.OnUseInstance(owner, tempCard.card.card, owner);
        }
    }
}