using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_MysticEyeVision_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 8;
        private BattleUnitModel _target;

        
        public override void AfterGiveDamage(int damage, BattleUnitModel target)
        {
            _target = target;
        }

        public override void OnEndBattle()
        {
            if (_target == null || _target.bufListDetail.GetActivatedBufList()
                .Count(x => x.bufType == KeywordBuf.Vulnerable) < Check) return;
            _target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable,10,owner);
        }
    }
}
