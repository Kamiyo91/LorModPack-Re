﻿using System.Linq;
using OldSamurai_Re21341.Buffs;
using Util_Re21341;

namespace OldSamurai_Re21341.Passives
{
    public class PassiveAbility_OldSamuraiEnemyDesc_Re21341 : PassiveAbilityBase
    {
        public override void OnDie()
        {
            if (!owner.bufListDetail.GetActivatedBufList()
                    .Exists(x => x is BattleUnitBuf_OldSamuraiEgoNpc_Re21341)) return;
            foreach (var ghostUnit in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x != owner))
            {
                ghostUnit.Die();
            }
        }
    }
}
