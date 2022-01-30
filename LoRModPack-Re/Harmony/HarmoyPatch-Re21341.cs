using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using BLL_Re21341.Models;
using HarmonyLib;
using LOR_DiceSystem;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Util_Re21341;
using Workshop;

namespace LoRModPack_Re21341.Harmony
{
    [HarmonyPatch]
    public class HarmoyPatch_Re21341
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIBookStoryChapterSlot), "SetEpisodeSlots")]
        public static void UIBookStoryChapterSlot_SetEpisodeSlots(UIBookStoryChapterSlot __instance,
            UIBookStoryPanel ___panel, List<UIBookStoryEpisodeSlot> ___EpisodeSlots)
        {
            SkinUtil.SetEpisodeSlots(__instance, ___panel, ___EpisodeSlots);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BookModel), "GetThumbSprite")]
        [HarmonyPatch(typeof(BookXmlInfo), "GetThumbSprite")]
        public static void General_GetThumbSprite(object __instance, ref Sprite __result)
        {
            switch (__instance)
            {
                case BookXmlInfo bookInfo:
                    SkinUtil.GetThumbSprite(bookInfo.id, ref __result);
                    break;
                case BookModel bookModel:
                    SkinUtil.GetThumbSprite(bookModel.BookId, ref __result);
                    break;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIBookStoryPanel), "OnSelectEpisodeSlot")]
        public static void UIBookStoryPanel_OnSelectEpisodeSlot(UIBookStoryPanel __instance,
            UIBookStoryEpisodeSlot slot, TextMeshProUGUI ___selectedEpisodeText, Image ___selectedEpisodeIcon,
            Image ___selectedEpisodeIconGlow)
        {
            if (slot == null || slot.books.Find(x => x.id.packageId == ModParameters.PackageId) == null) return;
            ___selectedEpisodeText.text = ModParameters.EffectTexts
                .FirstOrDefault(x => x.Key.Equals("CredenzaName_Re21341")).Value
                .Name;
            ___selectedEpisodeIcon.sprite = ModParameters.ArtWorks["Light_Re21341"];
            ___selectedEpisodeIconGlow.sprite = ModParameters.ArtWorks["Light_Re21341"];
            __instance.UpdateBookSlots();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIBattleSettingPanel), "SetToggles")]
        public static void UIBattleSettingPanel_SetToggles(UIBattleSettingPanel __instance)
        {
            if (!Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId
                    .Contains(ModParameters.PackageId)) return;
            if (!ModParameters.PreBattleUnits.ContainsKey(Singleton<StageController>.Instance.GetStageModel().ClassInfo
                    .id.id)) return;
            foreach (var currentAvailbleUnitslot in __instance.currentAvailbleUnitslots)
            {
                currentAvailbleUnitslot.SetToggle(false);
                currentAvailbleUnitslot.SetYesToggleState();
            }

            __instance.SetAvailibleText();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BookModel), "SetXmlInfo")]
        public static void BookModel_SetXmlInfo(BookModel __instance, ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId != ModParameters.PackageId) return;
            var onlyCards = ModParameters.OnlyCardKeywords.FirstOrDefault(x => x.Item3 == __instance.BookId.id)?.Item2;
            if (onlyCards != null)
                ____onlyCards.AddRange(onlyCards.Select(id =>
                    ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id))));
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitDataModel), "IsLockUnit")]
        public static void UnitDataModel_IsLockUnit(UnitDataModel __instance, ref bool __result,
            SephirahType ____ownerSephirah)
        {
            if (UI.UIController.Instance.CurrentUIPhase != UIPhase.BattleSetting) return;
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            if (stageModel == null || stageModel.ClassInfo.id.packageId != ModParameters.PackageId) return;
            if (ModParameters.OnlySephirahStage.Contains(stageModel.ClassInfo.id.id))
                __result = !__instance.isSephirah && ____ownerSephirah != SephirahType.None;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StageLibraryFloorModel), "InitUnitList")]
        public static void StageLibraryFloorModel_InitUnitList(StageLibraryFloorModel __instance,
            List<UnitBattleDataModel> ____unitList, StageModel stage)
        {
            if (stage.ClassInfo.id.packageId != ModParameters.PackageId) return;
            switch (stage.ClassInfo.id.id)
            {
                case 6:
                    if (__instance.Sephirah == SephirahType.Keter) ____unitList.Clear();
                    UnitUtil.AddCustomUnits(__instance, stage, ____unitList, 6);
                    return;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(WorkshopSkinDataSetter), "SetMotionData")]
        public static void WorkshopSkinDataSetter_SetMotionData(WorkshopSkinDataSetter __instance, ActionDetail motion,
            ClothCustomizeData data)
        {
            if (__instance.Appearance.GetCharacterMotion(motion) != null ||
                !ModParameters.SkinParameters.Exists(x => data.spritePath.Contains(x.Name))) return;
            var item = SkinUtil.CopyCharacterMotion(__instance.Appearance, motion);
            __instance.Appearance._motionList.Add(item);
            if (__instance.Appearance._motionList.Count <= 0) return;
            foreach (var characterMotion in __instance.Appearance._motionList.Where(characterMotion =>
                         !__instance.Appearance.CharacterMotions.ContainsKey(characterMotion.actionDetail)))
            {
                __instance.Appearance.CharacterMotions.Add(characterMotion.actionDetail, characterMotion);
                characterMotion.gameObject.SetActive(false);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UnitDataModel), "EquipBook")]
        public static void UnitDataModel_EquipBookPrefix(UnitDataModel __instance, bool force)
        {
            if (force) return;
            if (ModParameters.PackageId == __instance.bookItem.ClassInfo.id.packageId &&
                ModParameters.DynamicNames.ContainsKey(__instance.bookItem.ClassInfo.id.id)) __instance.ResetTempName();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitDataModel), "EquipBook")]
        public static void UnitDataModel_EquipBookPostfix(UnitDataModel __instance, BookModel newBook, bool force)
        {
            if (force) return;
            if (newBook != null && newBook.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.SkinNameIds.Any(x =>
                    x.Item2.Contains(newBook.ClassInfo.id.id) && newBook.ClassInfo.CharacterSkin.Contains(x.Item1)))
                newBook.ClassInfo.CharacterSkin = new List<string>
                {
                    ModParameters.SkinNameIds.FirstOrDefault(x => newBook.ClassInfo.CharacterSkin.Contains(x.Item1))
                        ?.Item3
                };
            if (newBook == null || ModParameters.PackageId != newBook.ClassInfo.workshopID ||
                !ModParameters.DynamicNames.ContainsKey(newBook.ClassInfo.id.id)) return;
            __instance.EquipCustomCoreBook(null);
            __instance.workshopSkin = "";
            var nameId = ModParameters.DynamicNames[newBook.ClassInfo.id.id].ToString();
            __instance.SetTempName(ModParameters.NameTexts[nameId]);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitDataModel), "LoadFromSaveData")]
        public static void UnitDataModel_LoadFromSaveData(UnitDataModel __instance)
        {
            if ((!string.IsNullOrEmpty(__instance.workshopSkin) || __instance.bookItem != __instance.CustomBookItem) &&
                __instance.bookItem.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.DynamicNames.ContainsKey(__instance.bookItem.ClassInfo.id.id))
                __instance.ResetTempName();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UICustomizePopup), "OnClickSave")]
        public static void UICustomizePopup_OnClickSave(UICustomizePopup __instance)
        {
            if (__instance.SelectedUnit.bookItem.ClassInfo.id.packageId != ModParameters.PackageId ||
                !ModParameters.DynamicNames.ContainsKey(__instance.SelectedUnit.bookItem.ClassInfo.id.id)) return;
            var tempName =
                (string)__instance.SelectedUnit.GetType().GetField("_tempName", AccessTools.all)
                    ?.GetValue(__instance.SelectedUnit);
            __instance.SelectedUnit.ResetTempName();
            if (__instance.SelectedUnit.bookItem == __instance.SelectedUnit.CustomBookItem &&
                string.IsNullOrEmpty(__instance.SelectedUnit.workshopSkin))
            {
                __instance.previewData.Name = __instance.SelectedUnit.name;
                var nameId = ModParameters.DynamicNames[__instance.SelectedUnit.bookItem.ClassInfo.id.id].ToString();
                __instance.SelectedUnit.SetTempName(ModParameters.NameTexts[nameId]);
            }
            else
            {
                if (string.IsNullOrEmpty(tempName) || __instance.previewData.Name == tempName)
                    __instance.previewData.Name = __instance.SelectedUnit.name;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(FarAreaEffect_Xiao_Taotie), "LateInit")]
        public static void FarAreaEffect_Xiao_Taotie_LateInit(BattleUnitModel ____self)
        {
            if (____self.UnitData.unitData.bookItem.ClassInfo.id.packageId != ModParameters.PackageId) return;
            if (____self.UnitData.unitData.bookItem.ClassInfo.id.id == 10000004 ||
                ____self.UnitData.unitData.bookItem.ClassInfo.id.id == 10000901 ||
                ____self.UnitData.unitData.bookItem.ClassInfo.id.id == 4)
                ____self.view.charAppearance.ChangeMotion(ActionDetail.Guard);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TextDataModel), "InitTextData")]
        public static void TextDataModel_InitTextData(string currentLanguage)
        {
            ModParameters.Language = currentLanguage;
            LocalizeUtil.AddLocalize();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UISettingInvenEquipPageListSlot), "SetBooksData")]
        [HarmonyPatch(typeof(UIInvenEquipPageListSlot), "SetBooksData")]
        public static void General_SetBooksData(object __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            var uiOrigin = __instance as UIOriginEquipPageList;
            SkinUtil.SetBooksData(uiOrigin, books, storyKey);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UISpriteDataManager), "Init")]
        public static void UISpriteDataManager_Init(UISpriteDataManager __instance)
        {
            foreach (var artWork in ModParameters.ArtWorks.Where(x =>
                         !x.Key.Contains("Glow") && !__instance._storyicons.Exists(y => y.type.Equals(x.Key))))
                __instance._storyicons.Add(new UIIconManager.IconSet
                {
                    type = artWork.Key,
                    icon = artWork.Value,
                    iconGlow = ModParameters.ArtWorks.FirstOrDefault(x => x.Key.Equals($"{artWork.Key}Glow")).Value ??
                               artWork.Value
                });
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BattleUnitView), "ChangeSkin")]
        public static void BattleUnitView_ChangeSkin(BattleUnitView __instance, string charName)
        {
            if (!ModParameters.SkinNameIds.Exists(x => x.Item1.Contains(charName))) return;
            var skinInfo =
                typeof(BattleUnitView).GetField("_skinInfo", AccessTools.all)?.GetValue(__instance) as
                    BattleUnitView.SkinInfo;
            skinInfo.state = BattleUnitView.SkinState.Changed;
            skinInfo.skinName = charName;
            var currentMotionDetail = __instance.charAppearance.GetCurrentMotionDetail();
            __instance.DestroySkin();
            var gameObject =
                Object.Instantiate(
                    Singleton<AssetBundleManagerRemake>.Instance.LoadCharacterPrefab(charName, "",
                        out var resourceName), __instance.model.view.characterRotationCenter);
            var workshopBookSkinData =
                Singleton<CustomizingBookSkinLoader>.Instance.GetWorkshopBookSkinData(
                    ModParameters.PackageId, charName);
            gameObject.GetComponent<WorkshopSkinDataSetter>().SetData(workshopBookSkinData);
            __instance.charAppearance = gameObject.GetComponent<CharacterAppearance>();
            __instance.charAppearance.Initialize(resourceName);
            __instance.charAppearance.ChangeMotion(currentMotionDetail);
            __instance.charAppearance.ChangeLayer("Character");
            __instance.charAppearance.SetLibrarianOnlySprites(__instance.model.faction);
            __instance.model.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { charName };
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(DropBookInventoryModel), "LoadFromSaveData")]
        public static void DropBookInventoryModel_LoadFromSaveData(DropBookInventoryModel __instance)
        {
            var bookCount = __instance.GetBookCount(new LorId(ModParameters.PackageId, 6));
            if (bookCount < 99) __instance.AddBook(new LorId(ModParameters.PackageId, 6), 99 - bookCount);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StageController), "BonusRewardWithPopup")]
        public static void BonusRewardWithPopup(LorId stageId)
        {
            if (stageId.packageId != ModParameters.PackageId) return;
            if (!ModParameters.ExtraReward.ContainsKey(stageId.id)) return;
            var message = false;
            var parameters = ModParameters.ExtraReward.FirstOrDefault(y => y.Key.Equals(stageId.id));
            if (parameters.Value.DroppedBooks != null)
            {
                message = true;
                foreach (var book in parameters.Value.DroppedBooks)
                    Singleton<DropBookInventoryModel>.Instance.AddBook(new LorId(ModParameters.PackageId, book.BookId),
                        book.Quantity);
            }

            if (parameters.Value.DroppedKeypages != null)
                foreach (var keypageId in parameters.Value.DroppedKeypages.Where(keypageId =>
                             !Singleton<BookInventoryModel>.Instance.GetBookListAll().Exists(x =>
                                 x.GetBookClassInfoId() == new LorId(ModParameters.PackageId, keypageId))))
                {
                    if (!message) message = true;
                    Singleton<BookInventoryModel>.Instance.CreateBook(new LorId(ModParameters.PackageId, keypageId));
                }

            if (message)
                UIAlarmPopup.instance.SetAlarmText(ModParameters.EffectTexts.FirstOrDefault(x =>
                        x.Key.Contains(parameters.Value.MessageId))
                    .Value
                    .Desc);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BattleUnitEmotionDetail), "ApplyEmotionCard")]
        public static bool BattleUnitEmotionDetail_ApplyEmotionCard(BattleUnitEmotionDetail __instance,
            BattleUnitModel ____self)
        {
            return !ModParameters.BannedEmotionSelectionUnit.Contains(____self.UnitData.unitData.bookItem.BookId);
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BattleUnitCardsInHandUI), "UpdateCardList")]
        public static void BattleUnitCardsInHandUI_UpdateCardList(BattleUnitCardsInHandUI __instance,
            List<BattleDiceCardUI> ____activatedCardList, ref float ____xInterval)
        {
            if (__instance.CurrentHandState != BattleUnitCardsInHandUI.HandState.EgoCard) return;
            var unit = __instance.SelectedModel ?? __instance.HOveredModel;
            if (unit.UnitData.unitData.bookItem.BookId.packageId != ModParameters.PackageId ||
                !ModParameters.NoEgoFloorUnit.Contains(unit.UnitData.unitData.bookItem.BookId.id)) return;
            var list = SkinUtil.ReloadEgoHandUI(__instance, __instance.GetCardUIList(), unit, ____activatedCardList,
                ref ____xInterval).ToList();
            __instance.SetSelectedCardUI(null);
            for (var i = list.Count; i < __instance.GetCardUIList().Count; i++)
                __instance.GetCardUIList()[i].gameObject.SetActive(false);
        }
    }
}