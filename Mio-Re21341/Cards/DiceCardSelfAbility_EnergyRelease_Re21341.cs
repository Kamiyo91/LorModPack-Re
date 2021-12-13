using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util_Re21341.CommonBuffs;

namespace Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_EnergyRelease_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard() => owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, owner);

        public override void OnStartBattle()
        {
            owner.bufListDetail.RemoveBufAll(KeywordBuf.Binding);
            owner.bufListDetail.AddBuf(new BattleUnitBuf_ImmunityToBinding_Re21341());
        }
    }
}
