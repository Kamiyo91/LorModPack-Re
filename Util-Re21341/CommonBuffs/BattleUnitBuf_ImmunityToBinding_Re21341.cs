using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmunityToBinding_Re21341 : BattleUnitBuf
    {
        public override bool IsImmune(KeywordBuf buf) => buf == KeywordBuf.Binding;

        public override void OnRoundEnd() => Destroy();
    }
}
