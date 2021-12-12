using BLL_Re21341.Models;
using LOR_DiceSystem;
using OldSamurai_Re21341.Buffs;
using OldSamurai_Re21341.Cards;
using Util_Re21341.CommonBuffs;

namespace OldSamurai_Re21341.Passives
{
    public class PassiveAbility_ControlledBreathing_Re21341 : PassiveAbilityBase
    {
        private int _count;
        private int _enemyCount = 3;
        private bool _lightUse;

        private void AddDeepBreathingCard()
        {
            _ = owner.personalEgoDetail.GetHand().Exists(x => x.GetID() == new LorId(ModParameters.PackageId, 1))
                ? _count = 0
                : _count++;
            if (_count != 3) return;
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 900));
            _count = 0;
        }

        private void RemoveDeepBreathingBuff()
        {
            if (owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_DeepBreathing_Re21341) is
                PassiveAbility_DeepBreathing_Re21341
                passiveAbilityDeepBreath)
                owner.passiveDetail.DestroyPassive(passiveAbilityDeepBreath);
        }

        public override void OnRoundEnd()
        {
            AddDeepBreathingCard();
            RemoveDeepBreathingBuff();
        }

        public override void OnRoundStart()
        {
            if (!_lightUse) return;
            _lightUse = false;
            owner.cardSlotDetail.SpendCost(2);
        }

        public override void OnRoundEndTheLast()
        {
            if (owner.faction != Faction.Enemy && !owner.bufListDetail.GetActivatedBufList()
                .Exists(x => x is BattleUnitBuf_Uncontrollable_Re21341))
                return;

            _enemyCount++;
            if (owner.RollSpeedDice().FindAll(x => !x.breaked).Count <= 0 || owner.IsBreakLifeZero()) return;

            if (_enemyCount <= 2 || owner.cardSlotDetail.PlayPoint < 2) return;
            UseDeepBreathingCardNpc();
        }

        private void UseDeepBreathingCardNpc()
        {
            _lightUse = true;
            _enemyCount = 0;
            owner.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 1));
            DiceCardSelfAbility_DeepBreathing_Re21341.Activate(owner);
        }

        public override void OnWaveStart()
        {
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 1));
        }


        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            owner.currentDiceAction.ignorePower = true;
            EmotionDiceRoll(behavior);
            AtkDiceRoll(behavior);
            DefDiceRoll(behavior);
            PowerNullDiceRoll(behavior);
        }

        private void EmotionDiceRoll(BattleDiceBehavior behavior)
        {
            var diceMaxRollAdder = 0;
            if (owner.emotionDetail.EmotionLevel > 2)
                diceMaxRollAdder = 1;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { min = 1, max = diceMaxRollAdder });
        }

        private void AtkDiceRoll(BattleDiceBehavior behavior)
        {
            var positiveNum = 0;
            if (behavior.Detail == BehaviourDetail.Slash || behavior.Detail == BehaviourDetail.Hit ||
                behavior.Detail == BehaviourDetail.Penetrate)
            {
                positiveNum = owner.bufListDetail.GetKewordBufStack(KeywordBuf.Strength);
                if (positiveNum > 0)
                    positiveNum /= 3;
            }
            behavior.ApplyDiceStatBonus(new DiceStatBonus { min = positiveNum, max = positiveNum });
        }

        private void DefDiceRoll(BattleDiceBehavior behavior)
        {
            var positiveNum = 0;
            if (behavior.Detail == BehaviourDetail.Guard || behavior.Detail == BehaviourDetail.Evasion)
            {
                positiveNum = owner.bufListDetail.GetKewordBufStack(KeywordBuf.Endurance);
                if (positiveNum > 0)
                    positiveNum /= 3;
            }
            behavior.ApplyDiceStatBonus(new DiceStatBonus { min=positiveNum,max = positiveNum });
        }

        private void PowerNullDiceRoll(BattleDiceBehavior behavior)
        {
            if (!owner.bufListDetail.GetActivatedBufList().Exists(x => x.bufType == KeywordBuf.NullifyPower)) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { min = 1, max = 1 });
        }
    }
}
