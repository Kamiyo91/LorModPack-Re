using System.Collections.Generic;
using BLL_Re21341.Models.Enum;
using EmotionalBurstPassive_Re21341.Cards;

namespace Omori_Re21341.Passives
{
    public class PassiveAbility_OmoriNpc_Re21341 : PassiveAbilityBase
    {
        private readonly List<EmotionBufEnum> _emotionBufEnum = new List<EmotionBufEnum>
            { EmotionBufEnum.Neutral, EmotionBufEnum.Angry, EmotionBufEnum.Happy, EmotionBufEnum.Sad };

        private EnemyTeamStageManager_Omori_Re21341 _stageManager;

        public override void OnWaveStart()
        {
            if (Singleton<StageController>.Instance.EnemyStageManager is EnemyTeamStageManager_Omori_Re21341 manager)
                _stageManager = manager;
        }

        public override void OnRoundStartAfter()
        {
            var randomEmotion = RandomUtil.SelectOne(_emotionBufEnum);
            switch (randomEmotion)
            {
                case EmotionBufEnum.Neutral:
                    DiceCardSelfAbility_Neutral_Re21341.Activate(owner);
                    break;
                case EmotionBufEnum.Sad:
                    DiceCardSelfAbility_Sad_Re21341.Activate(owner);
                    break;
                case EmotionBufEnum.Angry:
                    DiceCardSelfAbility_Angry_Re21341.Activate(owner);
                    break;
                case EmotionBufEnum.Happy:
                    DiceCardSelfAbility_Happy_Re21341.Activate(owner);
                    break;
                case EmotionBufEnum.All:
                case EmotionBufEnum.Afraid:
                default:
                    DiceCardSelfAbility_Neutral_Re21341.Activate(owner);
                    break;
            }
        }

        public override void AfterTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (owner.hp < 2)
                owner.breakDetail.LoseBreakLife(attacker);
        }

        public override int GetMaxHpBonus()
        {
            if (_stageManager == null) return 0;
            switch (_stageManager.GetPhase())
            {
                case 0:
                    return 0;
                case 1:
                    return 35;
                case 2:
                    return 75;
                default:
                    return 150;
            }
        }

        public override int SpeedDiceNumAdder()
        {
            return _stageManager.GetPhase() + 3;
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _stageManager?.CallMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }
    }
}