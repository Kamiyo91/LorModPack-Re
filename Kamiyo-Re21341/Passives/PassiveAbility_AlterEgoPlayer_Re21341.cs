using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using Kamiyo_Re21341.Buffs;
using LOR_XML;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_AlterEgoPlayer_Re21341 : PassiveAbilityBase
    {
        private MechUtilBase _util;
        public override void OnBattleEnd()
        {
            if (_util.CheckSkinChangeIsActive())
                owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "KamiyoNormal_Re21341" };
        }

        public override void OnWaveStart()
        {
            _util = new MechUtilBase(new MechUtilBaseModel
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
                EgoType = typeof(BattleUnitBuf_AlterEgoRelease_Re21341),
                AdditionalPassiveId = new LorId(ModParameters.PackageId, 14),
                NearDeathBuffType = typeof(BattleUnitBuf_NearDeath_Re21341),
                EgoCardId = new LorId(ModParameters.PackageId, 17),
                EgoAttackCardId = new LorId(ModParameters.PackageId, 16),
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
                target.passiveDetail.PassiveList.Exists(x => x.id == new LorId(ModParameters.PackageId, 18)))
                owner.Die();
        }

        public void SetDieAtEnd()
        {
            _util.TurnOnDieAtFightEnd();
        }

        public void ForcedEgo()
        {
            owner.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 17));
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
        }
    }
}