﻿using LOR_DiceSystem;

namespace Wilton_Re21341.Passives
{
    public class PassiveAbility_MysticEyes_Re21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash)
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, owner);
            if (behavior.Detail == BehaviourDetail.Penetrate)
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, 1, owner);
        }
    }
}