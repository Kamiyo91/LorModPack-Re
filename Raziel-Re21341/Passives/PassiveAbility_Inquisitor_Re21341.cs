using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;

namespace Raziel_Re21341.Passives
{
    public class PassiveAbility_Inquisitor_Re21341 : PassiveAbilityBase
    {
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = 1,
                dmgRate = 50
            });
        }

        public override void OnWaveStart()
        {
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId,1));
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 1));
        }
    }
}
