using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using Kamiyo_Re21341.Buffs;
using Kamiyo_Re21341.MapManager;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;
using Util_Re21341.Extentions;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_AlterEgoPlayer_Re21341 : PassiveAbilityBase
    {
        private MechUtilBaseEx _util;

        public override void OnBattleEnd()
        {
            if (_util.CheckSkinChangeIsActive())
                owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "KamiyoNormal_Re21341" };
        }

        public override void OnWaveStart()
        {
            _util = new MechUtilBaseEx(new MechUtilBaseModel
            {
                Owner = owner,
                Hp = 25,
                SetHp = 25,
                Survive = true,
                HasEgo = true,
                HasEgoAttack = true,
                HasAdditionalPassive = true,
                RefreshUI = true,
                NearDeathBuffExist = true,
                RecoverLightOnSurvive = false,
                SkinName = "KamiyoMask_Re21341",
                EgoMapName = "Kamiyo2_Re21341",
                EgoMapType = typeof(Kamiyo2_Re21341MapManager),
                BgY = 0.475f,
                FlY = 0.225f,
                OriginalMapStageIds = new List<LorId>
                    { new LorId(KamiyoModParameters.PackageId, 3), new LorId(KamiyoModParameters.PackageId, 12) },
                EgoType = typeof(BattleUnitBuf_AlterEgoRelease_Re21341),
                AdditionalPassiveId = new LorId(KamiyoModParameters.PackageId, 14),
                NearDeathBuffType = typeof(BattleUnitBuf_NearDeath_Re21341),
                EgoCardId = new LorId(KamiyoModParameters.PackageId, 17),
                EgoAttackCardId = new LorId(KamiyoModParameters.PackageId, 16),
                HasEgoAbDialog = true,
                HasSurviveAbDialog = true,
                SurviveAbDialogColor = AbColorType.Negative,
                EgoAbColorColor = AbColorType.Negative,
                SurviveAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Kamiyo",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoSurvive1_Re21341"))
                            .Value.Desc
                    },
                    new AbnormalityCardDialog
                    {
                        id = "Kamiyo",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoSurvive2_Re21341"))
                            .Value.Desc
                    }
                },
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Kamiyo",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive1_Re21341"))
                            .Value.Desc
                    },
                    new AbnormalityCardDialog
                    {
                        id = "Kamiyo",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive2_Re21341"))
                            .Value.Desc
                    },
                    new AbnormalityCardDialog
                    {
                        id = "Kamiyo",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive3_Re21341"))
                            .Value.Desc
                    }
                }
            });
            if (UnitUtil.CheckSkinProjection(owner))
                _util.DoNotChangeSkinOnEgo();
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(KamiyoModParameters.PackageId, 12),
                    typeof(BattleUnitBuf_AlterEgoRelease_Re21341))) _util.ForcedEgo();
        }

        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>()) return;
            owner.personalEgoDetail.RemoveCard(new LorId(KamiyoModParameters.PackageId, 60));
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 60));
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.SurviveCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnStartBattle()
        {
            UnitUtil.RemoveImmortalBuff(owner);
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnKill(BattleUnitModel target)
        {
            if (_util.CheckOnDieAtFightEnd() &&
                target.passiveDetail.PassiveList.Exists(x => x.id == new LorId(KamiyoModParameters.PackageId, 18)))
                owner.Die();
        }

        public void SetDieAtEnd()
        {
            _util.TurnOnDieAtFightEnd();
        }

        public void ForcedEgo()
        {
            owner.personalEgoDetail.RemoveCard(new LorId(KamiyoModParameters.PackageId, 17));
            _util.ForcedEgo();
            _util.ChangeEgoAbDialog(new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "Kamiyo",
                    dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive4_Re21341"))
                        .Value.Desc
                }
            });
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }

        public override void OnRoundEnd()
        {
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(KamiyoModParameters.PackageId, 12),
                    typeof(BattleUnitBuf_AlterEgoRelease_Re21341))) _util.ForcedEgo();
        }
    }
}