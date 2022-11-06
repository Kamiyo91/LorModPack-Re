using BigDLL4221.Buffs;

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
            owner.bufListDetail.AddBuf(new BattleUnitBuf_ImmunityToOneStatus_DLL4221(immunityType: KeywordBuf.Binding));
        }
    }
}