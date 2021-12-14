using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmunityToStatusAliment_Re21341 : BattleUnitBuf
    {
        public override bool IsImmune(BufPositiveType posType) => posType == BufPositiveType.Negative;

        public override void OnRoundEnd() => Destroy();
    }
}
