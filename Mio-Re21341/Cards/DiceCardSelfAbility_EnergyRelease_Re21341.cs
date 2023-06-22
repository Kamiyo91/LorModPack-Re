using UtilLoader21341.Extensions;

namespace KamiyoModPack.Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_EnergyRelease_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, owner);
        }

        public override void OnStartBattle()
        {
            owner.bufListDetail.RemoveBufAll(KeywordBuf.Binding);
            owner.bufListDetail.AddBuf(
                new BattleUnitBuf_ImmunityToOneStatus_DLL21341(immunityType: KeywordBuf.Binding));
        }
    }
}