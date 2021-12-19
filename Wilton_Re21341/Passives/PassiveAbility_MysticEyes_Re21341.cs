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
            if (behavior.Detail != BehaviourDetail.Slash && behavior.Detail != BehaviourDetail.Penetrate) return;
            behavior.TargetDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding,1,owner);
            behavior.TargetDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, owner);
        }
    }
}
