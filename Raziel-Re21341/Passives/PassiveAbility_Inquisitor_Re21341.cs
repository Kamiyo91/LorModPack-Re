using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using LOR_XML;
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
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 57));
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 58));
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead() || _revive) return;
            _revive = true;
            UnitUtil.UnitReviveAndRecovery(owner, owner.MaxHp, false);
            UnitUtil.BattleAbDialog(owner.view.dialogUI,
                new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "RazielEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("RazielImmortal_Re21341")).Value.Desc
                    }
                }, AbColorType.Negative);
            owner.forceRetreat = true;
        }
    }
}