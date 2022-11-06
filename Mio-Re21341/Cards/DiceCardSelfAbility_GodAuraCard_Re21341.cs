using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Passives;
using KamiyoModPack.Util_Re21341.CommonPassives;

namespace KamiyoModPack.Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_GodAuraCard_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return (owner.emotionDetail.EmotionLevel >= KamiyoModParameters.EgoEmotionLevel ||
                    (owner.emotionDetail.EmotionLevel >= 2 &&
                     owner.passiveDetail.HasPassive<PassiveAbility_KurosawaEmblem_Re21341>()) ||
                    owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>()) &&
                   !owner.bufListDetail.HasAssimilation();
        }
    }
}