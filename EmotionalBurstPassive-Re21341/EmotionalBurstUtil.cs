using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using EmotionalBurstPassive_Re21341.Buffs;
using EmotionalBurstPassive_Re21341.Passives;

namespace EmotionalBurstPassive_Re21341
{
    public static class EmotionalBurstUtil
    {
        public static void RemoveEmotionalBurstCards(BattleUnitModel unit)
        {
            unit.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 32));
            unit.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 33));
            unit.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 34));
            unit.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 35));
        }

        public static void AddEmotionalBurstCard(BattleUnitModel unit, EmotionBufEnum type)
        {
            switch (type)
            {
                case EmotionBufEnum.Neutral:
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 35));
                    return;
                case EmotionBufEnum.Angry:
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 32));
                    return;
                case EmotionBufEnum.Happy:
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 34));
                    return;
                case EmotionBufEnum.Sad:
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 33));
                    return;
                case EmotionBufEnum.All:
                case EmotionBufEnum.Afraid:
                default:
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 32));
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 33));
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 34));
                    unit.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 35));
                    return;
            }
        }

        public static bool CheckEmotionPassives(BattleUnitModel unit)
        {
            return unit.passiveDetail.PassiveList.Exists(x => !x.destroyed && x is PassiveAbility_Neutral_Re21341) ||
                   unit.passiveDetail.PassiveList.Exists(x => !x.destroyed && x is PassiveAbility_Happy_Re21341) ||
                   unit.passiveDetail.PassiveList.Exists(x => !x.destroyed && x is PassiveAbility_Angry_Re21341) ||
                   unit.passiveDetail.PassiveList.Exists(x => !x.destroyed && x is PassiveAbility_Sad_Re21341);
        }

        public static void RemoveAllEmotionalPassives(BattleUnitModel unit,
            EmotionBufEnum type = EmotionBufEnum.Neutral)
        {
            if (type != EmotionBufEnum.Afraid)
                if (unit.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Afraid_Re21341) is
                    BattleUnitBuf_Afraid_Re21341 buf)
                    buf.Destroy();
            if (type != EmotionBufEnum.Neutral)
                if (unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Neutral_Re21341) is
                    PassiveAbility_Neutral_Re21341 passiveAbilityNeutral)
                {
                    passiveAbilityNeutral.RemoveBuff();
                    unit.passiveDetail.DestroyPassive(passiveAbilityNeutral);
                }

            if (type != EmotionBufEnum.Happy)
                if (unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Happy_Re21341) is
                    PassiveAbility_Happy_Re21341 passiveAbilityHappy)
                {
                    passiveAbilityHappy.RemoveBuff();
                    unit.passiveDetail.DestroyPassive(passiveAbilityHappy);
                }

            if (type != EmotionBufEnum.Angry)
                if (unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Angry_Re21341) is
                    PassiveAbility_Angry_Re21341 passiveAbilityAngry)
                {
                    passiveAbilityAngry.RemoveBuff();
                    unit.passiveDetail.DestroyPassive(passiveAbilityAngry);
                }

            if (type == EmotionBufEnum.Sad) return;
            if (!(unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Sad_Re21341) is
                    PassiveAbility_Sad_Re21341 passiveAbilitySad)) return;
            passiveAbilitySad.RemoveBuff();
            unit.passiveDetail.DestroyPassive(passiveAbilitySad);
        }

        public static void DecreaseStacksBufType(BattleUnitModel owner, KeywordBuf bufType, int stacks)
        {
            var buf = owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x.bufType == bufType);
            if (buf != null) buf.stack -= stacks;
            if (buf != null && buf.stack < 1) owner.bufListDetail.RemoveBuf(buf);
        }
    }
}