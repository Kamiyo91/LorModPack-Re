using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR_DiceSystem;

namespace Wilton_Re21341.Passives
{
    public class PassiveAbility_MysticEyes_Re21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash)
                behavior.TargetDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, owner);
            if (behavior.Detail == BehaviourDetail.Penetrate)
                behavior.TargetDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, 1, owner);
        }
    }
}
