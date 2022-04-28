using BLL_Re21341.Models;
using Util_Re21341.CommonPassives;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_TrueGodAuraRelease_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return (owner.emotionDetail.EmotionLevel >= KamiyoModParameters.EgoEmotionLevel ||
                    owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>()) &&
                   !owner.bufListDetail.HasAssimilation();
        }
    }
}