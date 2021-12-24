using Raziel_Re21341.Buffs;

namespace Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_Kaioken_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel > 2;
        }

        public override void OnApplyCard()
        {
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_OwlSpirit_Re21341());
        }

        public override void OnReleaseCard()
        {
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_OwlSpirit_Re21341));
        }
    }
}