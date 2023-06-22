using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Buffs;
using KamiyoModPack.Kamiyo_Re21341.MapManager;
using LOR_DiceSystem;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Passives
{
    public class PassiveAbility_AlterEgoPlayer_Re21341 : PassiveAbilityBase
    {
        private const string OriginalSkinName = "KamiyoNormal_Re21341";
        private const string EgoSkinName = "KamiyoMask_Re21341";
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly LorId _egoAdditionalCard = new LorId(KamiyoModParameters.PackageId, 60);
        private readonly LorId _egoAttackCard = new LorId(KamiyoModParameters.PackageId, 16);
        private readonly LorId _egoCard = new LorId(KamiyoModParameters.PackageId, 17);

        private readonly List<AbnormalityCardDialog> _surviveDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "Kamiyo",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("KamiyoSurvive1_Re21341")).Value?.Desc ?? ""
            },
            new AbnormalityCardDialog
            {
                id = "Kamiyo",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("KamiyoSurvive2_Re21341")).Value?.Desc ?? ""
            }
        };

        public bool CustomSkin;
        public bool EgoActive;
        public bool EgoActiveQueue;

        public List<AbnormalityCardDialog> EgoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "Kamiyo",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive1_Re21341")).Value?.Desc ?? ""
            },
            new AbnormalityCardDialog
            {
                id = "Kamiyo",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive2_Re21341")).Value?.Desc ?? ""
            },
            new AbnormalityCardDialog
            {
                id = "Kamiyo",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive3_Re21341")).Value?.Desc ?? ""
            }
        };

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "Kamiyo2_Re21341MapManager",
            Stage = "Kamiyo2_Re21341",
            Bgy = 0.475f,
            Fy = 0.225f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 3, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public bool Survived;
        public string SurvivedSavedId = "KamiyoPlayerSurviveSave21341";

        public override void OnWaveStart()
        {
            var tryGet = Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<bool>(SurvivedSavedId, out var survived);
            if (tryGet) Survived = survived;
            owner.AddBuff<BattleUnitBuf_Shock_Re21341>(1);
            if (UnitUtil.CheckSkinProjection(owner))
                CustomSkin = true;
            owner.personalEgoDetail.AddCard(_egoCard);
        }

        public override void OnRoundStart()
        {
            if (EgoActive)
            {
                owner.personalEgoDetail.RemoveCard(_egoAdditionalCard);
                owner.personalEgoDetail.AddCard(_egoAdditionalCard);
            }

            if (!EgoActiveQueue) return;
            EgoActiveQueue = false;
            EgoActived();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 17:
                    EgoActiveQueue = true;
                    break;
                case 16:
                    MapUtil.ChangeMapGeneric<Kamiyo2_Re21341MapManager>(_cmh, MapModel);
                    break;
            }
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            owner.SurviveCheck<BattleUnitBuf>(dmg, 0, ref Survived, 64, dialog: _surviveDialog, color: Color.red);
            if (Survived && !owner.bufListDetail.HasBuf<BattleUnitBuf_NearDeath_Re21341>())
                owner.AddBuff<BattleUnitBuf_NearDeath_Re21341>(0);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public void EgoActived()
        {
            owner.personalEgoDetail.RemoveCard(_egoCard);
            owner.EgoActive<BattleUnitBuf_AlterEgoRelease_Re21341>(ref EgoActive, CustomSkin ? "" : EgoSkinName, true,
                false, new List<LorId> { _egoAttackCard, _egoAdditionalCard }, EgoDialog, Color.red);
            if (!CustomSkin) ChangeDiceEffects(owner);
            if (!owner.passiveDetail.HasPassive<PassiveAbility_MaskOfPerception_Re21341>())
                owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 14));
        }

        public override void OnBattleEnd()
        {
            Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(SurvivedSavedId, Survived);
            if (!CustomSkin)
                owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { OriginalSkinName };
        }

        public static void ChangeDiceEffects(BattleUnitModel owner)
        {
            foreach (var card in owner.allyCardDetail.GetAllDeck())
            {
                card.CopySelf();
                foreach (var dice in card.GetBehaviourList())
                    ChangeCardDiceEffect(dice);
            }
        }

        private static void ChangeCardDiceEffect(DiceBehaviour dice)
        {
            switch (dice.EffectRes)
            {
                case "KamiyoHit_Re21341":
                    dice.EffectRes = "KamiyoHitEgo_Re21341";
                    break;
                case "KamiyoSlash_Re21341":
                    dice.EffectRes = "KamiyoSlashEgo_Re21341";
                    break;
                case "PierceKamiyo_Re21341":
                    dice.EffectRes = "PierceKamiyoMask_Re21341";
                    break;
            }
        }
    }
}