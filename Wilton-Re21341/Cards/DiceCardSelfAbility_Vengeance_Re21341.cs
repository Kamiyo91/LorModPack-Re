using BLL_Re21341.Models;
using Util_Re21341.CommonPassives;

namespace Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_Vengeance_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return (owner.emotionDetail.EmotionLevel >= ModParameters.EgoEmotionLevel ||
                    owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>()) &&
                   !owner.bufListDetail.HasAssimilation();
        }
    }
}