using KamiyoModPack.Util_Re21341.CommonPassives;

namespace KamiyoModPack.Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_Kaioken_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel > 2 ||
                   owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>();
        }
    }
}