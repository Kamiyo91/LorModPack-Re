using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_FireAbsorb_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn,3,owner);
            var positiveNum = owner.bufListDetail.GetKewordBufStack(KeywordBuf.Burn);
                if (positiveNum > 0)
                    positiveNum /= 3;
            owner.bufListDetail.RemoveBufAll(KeywordBuf.Burn);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength,positiveNum,owner);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, positiveNum, owner);
        }
    }
}
