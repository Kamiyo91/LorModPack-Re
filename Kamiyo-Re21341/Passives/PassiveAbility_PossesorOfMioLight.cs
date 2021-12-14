using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kamiyo_Re21341.Buffs;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_PossesorOfMioLight : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if(owner.passiveDetail.HasPassive<PassiveAbility_AlterEgoPlayer_Re21341>()) owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_PossessorOfMioLight());
            else owner.passiveDetail.DestroyPassive(this);
        }
    }
}
