using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
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
            ActiveHappy();
        }

        private void ActiveHappy()
        {
            EmotionalBurstUtil.RemoveAllEmotionalPassives(owner, EmotionBufEnum.Happy);
            if (owner.passiveDetail.PassiveList.Find(x =>
                    x is PassiveAbility_Happy_Re21341) is PassiveAbility_Happy_Re21341 passiveHappy)
            {
                var stacks = passiveHappy.GetStack();
                if (stacks >= 3) return;
                passiveHappy.ChangeNameAndSetStacks(stacks + 1);
                passiveHappy.InstantIncrease();
                return;
            }

            var passive =
                owner.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 29)) as
                    PassiveAbility_Happy_Re21341;
            passive?.ChangeNameAndSetStacks(1);
            passive?.AfterInit();
            owner.passiveDetail.OnCreated();
        }
    }
}