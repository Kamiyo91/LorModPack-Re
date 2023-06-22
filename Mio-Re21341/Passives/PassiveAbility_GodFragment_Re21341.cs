using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Buffs;
using KamiyoModPack.Util_Re21341.CommonBuffs;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragment_Re21341 : PassiveAbilityBase
    {
        private const string OriginalSkinName = "MioNormalEye_Re21341";
        private const string EgoSkinName = "MioRedEye_Re21341";
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly LorId _egoAttackCard = new LorId(KamiyoModParameters.PackageId, 10);
        private readonly LorId _egoCard = new LorId(KamiyoModParameters.PackageId, 9);

        private List<AbnormalityCardDialog> _egoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "Mio",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("MioEgoActive1_Re21341")).Value?.Desc ?? ""
            },
            new AbnormalityCardDialog
            {
                id = "Mio",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("MioEgoActive2_Re21341")).Value?.Desc ?? ""
            }
        };

        public bool CustomSkin;
        public bool EgoActive;
        public bool EgoActiveQueue;

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "Mio_Re21341MapManager",
            Stage = "Mio_Re21341",
            Bgy = 0.2f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 2, PackageId = KamiyoModParameters.PackageId },
                new LorIdRoot { Id = 9, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public int SpeedCount;

        public override void OnWaveStart()
        {
            SpeedCount = 0;
            if (UnitUtil.CheckSkinProjection(owner))
                CustomSkin = true;
            owner.personalEgoDetail.AddCard(_egoCard);
        }

        public override void OnRoundStart()
        {
            if (!EgoActiveQueue) return;
            EgoActiveQueue = false;
            ForcedEgo();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.CheckTargetSpeedByCard(1))
            {
                SpeedCount++;
                Mathf.Clamp(SpeedCount, 0, 15);
            }

            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 9:
                    EgoActiveQueue = true;
                    break;
                case 10:
                    MapUtil.ChangeMapGeneric<Mio_Re21341MapManager>(_cmh, MapModel);
                    break;
            }
        }

        public void StageEgoSettings()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id != 2 ||
                !owner.IsSupportCharCheck()) return;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_Vip_Re21341());
            _egoDialog = new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "Mio",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("MioEgoActive3_Re21341")).Value
                        .Desc
                }
            };
        }

        public override void OnRoundEndTheLast()
        {
            if (SpeedCount > 2) owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, SpeedCount / 5, owner);
        }

        public void ForcedEgo()
        {
            owner.personalEgoDetail.RemoveCard(_egoCard);
            StageEgoSettings();
            owner.EgoActive<BattleUnitBuf_GodAuraRelease_Re21341>(ref EgoActive, CustomSkin ? "" : EgoSkinName, true,
                false, new List<LorId> { _egoAttackCard }, _egoDialog, Color.green);
        }

        public override void OnBattleEnd()
        {
            if (!CustomSkin)
                owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { OriginalSkinName };
        }
    }
}