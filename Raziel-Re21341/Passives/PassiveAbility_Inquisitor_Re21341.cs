using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using Util_Re21341;

namespace Raziel_Re21341.Passives
{
    public class PassiveAbility_Inquisitor_Re21341 : PassiveAbilityBase
    {
        private bool _revive;
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
            _revive = false;
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId,57));
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 58));
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead() || _revive) return;
            _revive = true;
            UnitUtil.UnitReviveAndRecovery(owner,owner.MaxHp,false);
        }
    }
}
