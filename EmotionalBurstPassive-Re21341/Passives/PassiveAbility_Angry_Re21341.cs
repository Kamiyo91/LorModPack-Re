using System.Linq;
using BLL_Re21341.Models;
using EmotionalBurstPassive_Re21341.Buffs;
using Util_Re21341;

namespace EmotionalBurstPassive_Re21341.Passives
{
    public class PassiveAbility_Angry_Re21341 : PassiveAbilityBase
    {
        private int _stack;

        public int GetStack()
        {
            return _stack;
        }

        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            Hide();
        }

        public void ChangeNameAndSetStacks(int stack)
        {
            switch (stack)
            {
                case 1:
                    name = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Angry_Re21341")).Value.Name;
                    desc = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Angry_Re21341")).Value.Desc;
                    _stack = 1;
                    break;
                case 2:
                    name = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Enraged_Re21341")).Value.Name;
                    desc = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Enraged_Re21341")).Value.Desc;
                    _stack = 2;
                    break;
                case 3:
                    name = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Furious_Re21341")).Value.Name;
                    desc = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("Furious_Re21341")).Value.Desc;
                    _stack = 3;
                    break;
            }

            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Angry_Re21341) is
                BattleUnitBuf_Angry_Re21341 buf)
            {
                buf.stack = stack;
            }
            else
            {
                buf = new BattleUnitBuf_Angry_Re21341
                {
                    stack = stack
                };
                owner.bufListDetail.AddBufWithoutDuplication(buf);
            }
        }

        public override void OnRoundStartAfter()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, _stack);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Disarm, _stack);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, _stack * 3);
        }

        public void RemoveBuff()
        {
            EmotionalBurstUtil.DecreaseStacksBufType(owner, KeywordBuf.Strength, _stack);
            EmotionalBurstUtil.DecreaseStacksBufType(owner, KeywordBuf.Disarm, _stack);
            EmotionalBurstUtil.DecreaseStacksBufType(owner, KeywordBuf.Vulnerable, _stack * 3);
            owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_Angry_Re21341)?.Destroy();
        }

        public void InstantIncrease()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Disarm, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 3);
        }

        public void AfterInit()
        {
            OnRoundStartAfter();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            UnitUtil.SetPassiveCombatLog(this, owner);
            owner.battleCardResultLog?.AddEmotionCoin(EmotionCoinType.Negative,
                owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative));
            return base.BeforeTakeDamage(attacker, dmg);
        }
    }
}