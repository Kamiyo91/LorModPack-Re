using System.Linq;

namespace KamiyoModPack.Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_HorizonSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 4;
        private bool _atkSuccess;

        public override void OnUseCard()
        {
            _atkSuccess = false;
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }

        public override void OnSucceedAttack()
        {
            _atkSuccess = true;
        }

        public override void OnEndBattle()
        {
            if (!_atkSuccess || card.target.bufListDetail.GetActivatedBufList()
                    .Where(x => x.positiveType == BufPositiveType.Negative).Select(x => x.stack).Sum() < Check) return;
            card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, 1, owner);
            card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, owner);
        }
    }
}