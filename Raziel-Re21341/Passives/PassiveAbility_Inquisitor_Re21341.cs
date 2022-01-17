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
        private bool _skinChanged;
        private bool _used;

        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = 1,
                dmgRate = 25
            });
        }

        public override void OnWaveStart()
        {
            _skinChanged = UnitUtil.CheckSkinProjection(owner);
            _revive = false;
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 57));
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 58));
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (_used)
            {
                _used = false;
                MapUtil.ReturnFromEgoMap("Raziel_Re21341", 7);
            }

            if (!owner.IsDead() || _revive) return;
            _revive = true;
            UnitUtil.UnitReviveAndRecovery(owner, owner.MaxHp, false, _skinChanged);
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

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.card.GetID().packageId != ModParameters.PackageId) return;
            if (curCard.card.GetID().id != 58 || owner.faction != Faction.Player ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            _used = true;
            ChangeToRazielEgoMap();
        }

        private static void ChangeToRazielEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "Raziel_Re21341",
                StageId = 7,
                OneTurnEgo = true,
                IsPlayer = true,
                Component = typeof(Raziel_Re21341MapManager),
                Bgy = 0.375f,
                Fy = 0.225f
            });
        }

        public override void OnBattleEnd()
        {
            if (owner.IsDead() && !MapUtil.CheckStageMap(4)) UnitUtil.UnitReviveAndRecovery(owner, 5, false);
        }
    }
}