using System.Linq;

namespace Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_MysticEyeVision_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 5;
        private bool _atkSuccess;

        public override void OnUseCard()
        {
            _atkSuccess = false;
        }
        public override void OnSucceedAttack()
        {
            _atkSuccess = true;
        }

        public override void OnEndBattle()
        {
            if (!_atkSuccess || !card.target.bufListDetail.GetActivatedBufList().Exists(x => x.bufType == KeywordBuf.Vulnerable && x.stack >= Check)) return;
            card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 10, owner);
        }
    }
}