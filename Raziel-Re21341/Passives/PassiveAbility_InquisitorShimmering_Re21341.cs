using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raziel_Re21341.Passives
{
    public class PassiveAbility_InquisitorShimmering_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            owner.allyCardDetail.DrawCards(8);
        }

        public override int SpeedDiceNumAdder()
        {
            return 4;
        }
    }
}
