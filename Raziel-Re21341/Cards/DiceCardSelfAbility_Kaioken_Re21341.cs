using Raziel_Re21341.Buffs;
using Util_Re21341.CommonPassives;

namespace Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_Kaioken_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel > 2 ||
                   owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>();
        }

        public override void OnApplyCard()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_OwlSpirit_Re21341());
        }

        public override void OnReleaseCard()
        {
            owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_OwlSpirit_Re21341)?.Destroy();
        }
    }
}