﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BLL_Re21341.Models;
using HarmonyLib;
using LOR_DiceSystem;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Util_Re21341;
using Workshop;
using Object = UnityEngine.Object;

namespace LoRModPack_Re21341.Harmony
{
    public class LoRModPack_Re : ModInitializer
    {
        public override void OnInitializeMod()
        {
            ModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            var harmony = new HarmonyLib.Harmony("LOR.LorModPackRe21341_MOD");
            var method = typeof(LoRModPack_Re).GetMethod("BookModel_SetXmlInfo");
            harmony.Patch(typeof(BookModel).GetMethod("SetXmlInfo", AccessTools.all), null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("General_GetThumbSprite");
            harmony.Patch(typeof(BookModel).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            harmony.Patch(typeof(BookXmlInfo).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("StageLibraryFloorModel_InitUnitList");
            harmony.Patch(typeof(StageLibraryFloorModel).GetMethod("InitUnitList", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("General_SetBooksData");
            harmony.Patch(typeof(UISettingInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            harmony.Patch(typeof(UIInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UISpriteDataManager_Init");
            harmony.Patch(typeof(UISpriteDataManager).GetMethod("Init", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UIBookStoryChapterSlot_SetEpisodeSlots");
            harmony.Patch(typeof(UIBookStoryChapterSlot).GetMethod("SetEpisodeSlots", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UIBookStoryPanel_OnSelectEpisodeSlot");
            harmony.Patch(typeof(UIBookStoryPanel).GetMethod("OnSelectEpisodeSlot", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UIBattleSettingPanel_SetToggles");
            harmony.Patch(typeof(UIBattleSettingPanel).GetMethod("SetToggles", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("BattleUnitView_ChangeSkin");
            harmony.Patch(typeof(BattleUnitView).GetMethod("ChangeSkin", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UnitDataModel_EquipBookPrefix");
            var methodPostfix = typeof(LoRModPack_Re).GetMethod("UnitDataModel_EquipBookPostfix");
            harmony.Patch(typeof(UnitDataModel).GetMethod("EquipBook", AccessTools.all),
                new HarmonyMethod(method), new HarmonyMethod(methodPostfix));
            method = typeof(LoRModPack_Re).GetMethod("UnitDataModel_LoadFromSaveData");
            harmony.Patch(typeof(UnitDataModel).GetMethod("LoadFromSaveData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UICustomizePopup_OnClickSave");
            harmony.Patch(typeof(UICustomizePopup).GetMethod("OnClickSave", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("TextDataModel_InitTextData");
            harmony.Patch(typeof(TextDataModel).GetMethod("InitTextData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("WorkshopSkinDataSetter_SetMotionData");
            harmony.Patch(typeof(WorkshopSkinDataSetter).GetMethod("SetMotionData", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("StageLibraryFloorModel_StartPickEmotionCard");
            harmony.Patch(typeof(StageLibraryFloorModel).GetMethod("StartPickEmotionCard", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("StageWaveModel_PickRandomEmotionCard");
            harmony.Patch(typeof(StageWaveModel).GetMethod("PickRandomEmotionCard", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("FarAreaEffect_Xiao_Taotie_LateInit");
            harmony.Patch(typeof(FarAreaEffect_Xiao_Taotie).GetMethod("LateInit", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("DropBookInventoryModel_LoadFromSaveData");
            harmony.Patch(typeof(DropBookInventoryModel).GetMethod("LoadFromSaveData", AccessTools.all),
                null, new HarmonyMethod(method));
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            MapUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance);
            UnitUtil.ChangePassiveItem();
            SkinUtil.LoadBookSkinsExtra();
            SkinUtil.PreLoadBufIcons();
            LocalizeUtil.AddLocalize();
            LocalizeUtil.RemoveError();
        }

        public static void StageLibraryFloorModel_StartPickEmotionCard(StageLibraryFloorModel __instance)
        {
            if (!ModParameters.BannedEmotionStages.ContainsKey(Singleton<StageController>.Instance.GetStageModel()
                    .ClassInfo.id)) return;
            __instance.team.currentSelectEmotionLevel++;
            __instance.team.egoSelectionPoint--;
            SingletonBehavior<BattleManagerUI>.Instance.ui_levelup.SetRootCanvas(false);
            SingletonBehavior<BattleManagerUI>.Instance.ui_levelup.OnSelectHide(true);
        }

        public static void StageWaveModel_PickRandomEmotionCard(StageWaveModel __instance)
        {
            if (!ModParameters.BannedEmotionStages.ContainsKey(Singleton<StageController>.Instance.GetStageModel()
                    .ClassInfo.id) || ModParameters.BannedEmotionStages.FirstOrDefault(x =>
                    x.Key.Equals(Singleton<StageController>.Instance.GetStageModel().ClassInfo.id)).Value) return;
            __instance.team.currentSelectEmotionLevel++;
        }

        public static void UIBookStoryChapterSlot_SetEpisodeSlots(UIBookStoryChapterSlot __instance,
            UIBookStoryPanel ___panel, List<UIBookStoryEpisodeSlot> ___EpisodeSlots)
        {
            SkinUtil.SetEpisodeSlots(__instance, ___panel, ___EpisodeSlots);
        }

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

        public static void BookModel_SetXmlInfo(BookModel __instance, ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId != ModParameters.PackageId) return;
            var onlyCards = ModParameters.OnlyCardKeywords.FirstOrDefault(x => x.Item3 == __instance.BookId.id)?.Item2;
            if (onlyCards != null)
                ____onlyCards.AddRange(onlyCards.Select(id =>
                    ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id))));
        }

        public static void StageLibraryFloorModel_InitUnitList(StageLibraryFloorModel __instance,
            List<UnitBattleDataModel> ____unitList, StageModel stage)
        {
            if (stage.ClassInfo.id.packageId != ModParameters.PackageId) return;
            switch (stage.ClassInfo.id.id)
            {
                case 1:
                    ____unitList.Clear();
                    UnitUtil.AddUnitSephiraOnly(__instance, stage, ____unitList);
                    return;
                case 4:
                case 10:
                    ____unitList.Clear();
                    UnitUtil.AddUnitSephiraOnly(__instance, stage, ____unitList);
                    return;
                case 6:
                    if (__instance.Sephirah == SephirahType.Keter) ____unitList.Clear();
                    UnitUtil.AddCustomUnits(__instance, stage, ____unitList, 6);
                    return;
            }
        }

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

        public static void UnitDataModel_EquipBookPrefix(UnitDataModel __instance, BookModel newBook, bool force)
        {
            if (force) return;
            if (ModParameters.PackageId == __instance.bookItem.ClassInfo.id.packageId &&
                ModParameters.DynamicNames.ContainsKey(__instance.bookItem.ClassInfo.id.id)) __instance.ResetTempName();
        }

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

        public static void UnitDataModel_LoadFromSaveData(UnitDataModel __instance)
        {
            if ((!string.IsNullOrEmpty(__instance.workshopSkin) || __instance.bookItem != __instance.CustomBookItem) &&
                __instance.bookItem.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.DynamicNames.ContainsKey(__instance.bookItem.ClassInfo.id.id))
                __instance.ResetTempName();
        }

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

        public static void FarAreaEffect_Xiao_Taotie_LateInit(BattleUnitModel ____self)
        {
            if (____self.UnitData.unitData.bookItem.ClassInfo.id == new LorId(ModParameters.PackageId, 10000004))
                ____self.view.charAppearance.ChangeMotion(ActionDetail.Guard);
        }

        public static void TextDataModel_InitTextData(string currentLanguage)
        {
            ModParameters.Language = currentLanguage;
            LocalizeUtil.AddLocalize();
        }

        public static void General_SetBooksData(object __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            var uiOrigin = __instance as UIOriginEquipPageList;
            SkinUtil.SetBooksData(uiOrigin, books, storyKey);
        }

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

        public static void DropBookInventoryModel_LoadFromSaveData(DropBookInventoryModel __instance)
        {
            var bookCount = __instance.GetBookCount(new LorId(ModParameters.PackageId, 6));
            if (bookCount < 99) __instance.AddBook(new LorId(ModParameters.PackageId, 6), 99 - bookCount);
        }
    }
}