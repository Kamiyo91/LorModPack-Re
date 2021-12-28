using System.Linq;
using BLL_Re21341.Models;
using EmotionalBurstPassive_Re21341.Buffs;

namespace EmotionalBurstPassive_Re21341.Passives
{
    public class PassiveAbility_Sad_Re21341 : PassiveAbilityBase
    {
        private int _stack;

        public int GetStack()
        {
            return _stack;
        }

        public void ChangeNameAndSetStacks(int stack)
        {
            switch (stack)
            {
                case 1:
                    name = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Sad_Re21341")).Value.Name;
                    desc = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Sad_Re21341")).Value.Desc;
                    _stack = 1;
                    break;
                case 2:
                    name = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Depressed_Re21341")).Value.Name;
                    desc = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Depressed_Re21341")).Value.Desc;
                    _stack = 2;
                    break;
                case 3:
                    name = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Miserable_Re21341")).Value.Name;
                    desc = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Miserable_Re21341")).Value.Desc;
                    _stack = 3;
                    break;
            }

            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Sad_Re21341) is
                BattleUnitBuf_Sad_Re21341 buf)
            {
                buf.stack = stack;
            }
            else
            {
                buf = new BattleUnitBuf_Sad_Re21341
                {
                    stack = stack
                };
                owner.bufListDetail.AddBufWithoutDuplication(buf);
            }
        }

        public override void OnRoundStartAfter()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, _stack);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Binding, _stack);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, _stack * 2);
        }

        public void RemoveBuff()
        {
            EmotionalBurstUtil.DecreaseStacksBufType(owner, KeywordBuf.Endurance, _stack);
            EmotionalBurstUtil.DecreaseStacksBufType(owner, KeywordBuf.Binding, _stack);
            EmotionalBurstUtil.DecreaseStacksBufType(owner, KeywordBuf.Protection, _stack * 2);
            owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_Sad_Re21341)?.Destroy();
        }

        public void InstantIncrease()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Binding, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 2);
        }

        public void AfterInit()
        {
            OnRoundStartAfter();
        }

        public override void OnRoundEnd()
        {
            ChangeAllCoinsToNegativeType();
        }

        private void ChangeAllCoinsToNegativeType()
        {
            owner.emotionDetail.totalEmotionCoins.RemoveAll(x => x.CoinType == EmotionCoinType.Positive);
            var allEmotionCoins = owner.emotionDetail.AllEmotionCoins.Where(x => x.CoinType == EmotionCoinType.Positive)
                .ToList();
            foreach (var coin in allEmotionCoins)
            {
                owner.emotionDetail.AllEmotionCoins.Remove(coin);
                owner.battleCardResultLog?.AddEmotionCoin(EmotionCoinType.Negative,
                    owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative));
            }
        }
    }
}