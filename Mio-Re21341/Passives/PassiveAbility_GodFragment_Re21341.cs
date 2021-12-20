using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using LOR_XML;
using Mio_Re21341.Buffs;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragment_Re21341 : PassiveAbilityBase
    {
        private MechUtilBase _util;

        public override void OnBattleEnd()
        {
            if (_util.CheckSkinChangeIsActive())
                owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "MioNormalEye_Re21341" };
        }

        public override void OnWaveStart()
        {
            _util = new MechUtilBase(new MechUtilBaseModel
            {
                Owner = owner,
                Hp = 0,
                SetHp = 65,
                Survive = true,
                HasEgo = true,
                HasEgoAttack = true,
                RefreshUI = true,
                RecoverLightOnSurvive = false,
                SkinName = "MioRedEye_Re21341",
                EgoType = typeof(BattleUnitBuf_GodAuraRelease_Re21341),
                EgoCardId = new LorId(ModParameters.PackageId, 9),
                EgoAttackCardId = new LorId(ModParameters.PackageId, 10),
                HasEgoAbDialog = true,
                HasSurviveAbDialog = true,
                SurviveAbDialogColor = AbColorType.Negative,
                EgoAbColorColor = AbColorType.Positive,
                SurviveAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Mio",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("MioSurvive1_Re21341"))
                            .Value.Desc
                    },
                    new AbnormalityCardDialog
                    {
                        id = "Mio",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("MioSurvive2_Re21341"))
                            .Value.Desc
                    }
                },
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Mio",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("MioEgoActive1_Re21341"))
                            .Value.Desc
                    },
                    new AbnormalityCardDialog
                    {
                        id = "Mio",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("MioEgoActive2_Re21341"))
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

        public void ForcedEgo()
        {
            _util.SetVipUnit();
            _util.ChangeEgoAbDialog(new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "Mio",
                    dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("MioEgoActive3_Re21341")).Value
                        .Desc
                }
            });
            _util.ForcedEgo();
            owner.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 9));
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }
    }
}