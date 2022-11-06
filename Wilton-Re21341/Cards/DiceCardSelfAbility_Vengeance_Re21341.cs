using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Util_Re21341.CommonPassives;

namespace KamiyoModPack.Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_Vengeance_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return (owner.emotionDetail.EmotionLevel >= KamiyoModParameters.EgoEmotionLevel ||
                    owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>()) &&
                   !owner.bufListDetail.HasAssimilation();
        }
    }
}