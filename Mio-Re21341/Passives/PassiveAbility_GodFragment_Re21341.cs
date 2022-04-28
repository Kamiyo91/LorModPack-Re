using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;
using Mio_Re21341.Buffs;
using Util_Re21341.Extentions;

namespace Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragment_Re21341 : PassiveAbilityBase
    {
        private MechUtilBaseEx _util;

        public override void OnBattleEnd()
        {
            if (_util.CheckSkinChangeIsActive())
                owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "MioNormalEye_Re21341" };
        }

        public override void OnWaveStart()
        {
            _util = new MechUtilBaseEx(new MechUtilBaseModel
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
                EgoMapName = "Mio_Re21341",
                EgoMapType = typeof(Mio_Re21341MapManager),
                BgY = 0.2f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(KamiyoModParameters.PackageId, 2), new LorId(KamiyoModParameters.PackageId, 9),
                    new LorId(KamiyoModParameters.PackageId, 12)
                },
                EgoType = typeof(BattleUnitBuf_GodAuraRelease_Re21341),
                EgoCardId = new LorId(KamiyoModParameters.PackageId, 9),
                EgoAttackCardId = new LorId(KamiyoModParameters.PackageId, 10),
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
            owner.personalEgoDetail.RemoveCard(new LorId(KamiyoModParameters.PackageId, 9));
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
    }
}