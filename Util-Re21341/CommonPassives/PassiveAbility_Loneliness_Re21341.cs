using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_Loneliness_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (BattleObjectManager.instance.GetAliveList(owner.faction).Count == 1)
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 3);
        }
    }
}
