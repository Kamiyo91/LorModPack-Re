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
            if (owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Sad_Re21341) is PassiveAbility_Sad_Re21341 passive)
            {
                owner.RecoverHP(10 * passive.GetStack());
                owner.breakDetail.RecoverBreak(10 * passive.GetStack());
                owner.cardSlotDetail.RecoverPlayPoint(passive.GetStack());
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, passive.GetStack(), owner);
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, passive.GetStack(), owner);
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
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.hp < owner.MaxHp * 0.25f;
        }
    }
}