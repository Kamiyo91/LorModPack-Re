using System.Linq;
using UtilLoader21341.Util;

namespace KamiyoModPack.Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_HorizonSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 5;
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
            owner.ChangeSameCardsCost(card, -1);
            card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, 2, owner);
            card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 2, owner);
        }
    }
}