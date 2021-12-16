using System;
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
            method = typeof(LoRModPack_Re).GetMethod("BookModel_GetThumbSprite");
            harmony.Patch(typeof(BookModel).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("BookXmlInfo_GetThumbSprite");
            harmony.Patch(typeof(BookXmlInfo).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("StageLibraryFloorModel_InitUnitList");
            harmony.Patch(typeof(StageLibraryFloorModel).GetMethod("InitUnitList", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UISettingInvenEquipPageListSlot_SetBooksData");
            harmony.Patch(typeof(UISettingInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UIInvenEquipPageListSlot_SetBooksData");
            harmony.Patch(typeof(UIInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UISpriteDataManager_GetStoryIcon");
            harmony.Patch(typeof(UISpriteDataManager).GetMethod("GetStoryIcon", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("BattleUnitView_ChangeSkin");
            harmony.Patch(typeof(BattleUnitView).GetMethod("ChangeSkin", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UnitDataModel_EquipBook");
            harmony.Patch(typeof(UnitDataModel).GetMethod("EquipBook", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(LoRModPack_Re).GetMethod("UILibrarianAppearanceInfoPanel_OnClickCustomizeButton");
            harmony.Patch(typeof(UILibrarianAppearanceInfoPanel).GetMethod("OnClickCustomizeButton", AccessTools.all),
                new HarmonyMethod(method));
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            MapUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance);
            UnitUtil.ChangePassiveItem();
            LocalizeUtil.AddLocalize();
            LocalizeUtil.RemoveError();
        }

        public static void BookXmlInfo_GetThumbSprite(BookXmlInfo __instance, ref Sprite __result)
        {
            if (__instance.id.packageId != ModParameters.PackageId) return;
            switch (__instance.id.id)
            {
                case 10000001:
                case 10000002:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/243003");
                    return;
                case 10000003:
                    __result = ModParameters.ArtWorks["MioDefault_Re21341"];
                    return;
                case 10000004:
                    __result = ModParameters.ArtWorks["KamiyoDefault_Re21341"];
                    return;
                case 10000005:
                    __result = ModParameters.ArtWorks["HayateDefault_Re21341"];
                    return;
                case 10000006:
                    __result = ModParameters.ArtWorks["AngelaDefault_Re21341"];
                    return;
                case 10000007:
                case 10000008:
                case 10000009:
                case 10000010:
                case 10000011:
                    __result = ModParameters.ArtWorks["FragmentDefault_Re21341"];
                    return;
                default:
                    return;
            }
        }

        public static void BookModel_GetThumbSprite(BookModel __instance, ref Sprite __result)
        {
            if (__instance.BookId.packageId != ModParameters.PackageId) return;
            switch (__instance.BookId.id)
            {
                case 10000001:
                case 10000002:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/243003");
                    return;
                case 10000003:
                    __result = ModParameters.ArtWorks["MioDefault_Re21341"];
                    return;
                case 10000004:
                    __result = ModParameters.ArtWorks["KamiyoDefault_Re21341"];
                    return;
                case 10000005:
                    __result = ModParameters.ArtWorks["HayateDefault_Re21341"];
                    return;
                case 10000006:
                    __result = ModParameters.ArtWorks["AngelaDefault_Re21341"];
                    return;
                case 10000007:
                case 10000008:
                case 10000009:
                case 10000010:
                case 10000011:
                    __result = ModParameters.ArtWorks["FragmentDefault_Re21341"];
                    return;
                default:
                    return;
            }
        }

        public static void BookModel_SetXmlInfo(BookModel __instance, ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId == ModParameters.PackageId &&
                ModParameters.KeypageWithOnlyCardsList.Keys.Contains(__instance.BookId.id))
                ____onlyCards.AddRange(ModParameters.KeypageWithOnlyCardsList
                    .FirstOrDefault(x => x.Key.Equals(__instance.BookId.id)).Value.Select(id =>
                        ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id))));
        }

        public static void StageLibraryFloorModel_InitUnitList(StageLibraryFloorModel __instance, StageModel stage,
            LibraryFloorModel floor)
        {
            if (stage.ClassInfo.id.packageId != ModParameters.PackageId) return;
            foreach (var unitDataModel in floor.GetUnitDataList())
                switch (stage.ClassInfo.id.id)
                {
                    case 1:
                        UnitUtil.ClearCharList(__instance);
                        UnitUtil.AddUnitSephiraOnly(__instance, stage, unitDataModel);
                        return;
                    case 4:
                        UnitUtil.ClearCharList(__instance);
                        UnitUtil.AddUnitSephiraOnly(__instance, stage, unitDataModel);
                        return;
                }
        }

        public static bool UnitDataModel_EquipBook(UnitDataModel __instance, BookModel newBook, bool isEnemySetting,
            bool force)
        {
            if (!force && newBook != null && newBook.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.EquipPageWithOriginalFace.Contains(newBook.ClassInfo.id.id) && newBook.owner == null)
            {
                __instance.customizeData.SetCustomData(false);
                __instance.EquipCustomCoreBook(null);
                ModParameters.DynamicNames.TryGetValue(newBook.ClassInfo.id.id, out var name);
                __instance.SetTempName(ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals(name)).Value.Name);
            }
            else if (__instance.bookItem.ClassInfo.id.packageId == ModParameters.PackageId &&
                     ModParameters.EquipPageWithOriginalFace.Contains(__instance.bookItem.ClassInfo.id.id))
            {
                __instance.customizeData.SetCustomData(true);
                __instance.ResetTempName();
            }

            return force || newBook == null || newBook.ClassInfo.id.packageId != ModParameters.PackageId ||
                   !ModParameters.NotEquipPages.Contains(newBook.ClassInfo.id.id) || newBook.owner != null;
        }

        public static bool UILibrarianAppearanceInfoPanel_OnClickCustomizeButton(
            UILibrarianAppearanceInfoPanel __instance)
        {
            if (__instance.unitData.bookItem.BookId.packageId != ModParameters.PackageId ||
                !ModParameters.WithoutCustomSkins.Contains(__instance.unitData.bookItem.BookId.id)) return true;
            UIAlarmPopup.instance.SetAlarmText(ModParameters.EffectTexts
                .FirstOrDefault(x => x.Key.Equals("AngelaName_Re21341")).Value.Desc);
            return false;
        }

        public static void UIInvenEquipPageListSlot_SetBooksData(UISettingInvenEquipPageListSlot __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            if (storyKey.workshopId != ModParameters.PackageId) return;
            var image = (Image)__instance.GetType().GetField("img_IconGlow", AccessTools.all).GetValue(__instance);
            var image2 = (Image)__instance.GetType().GetField("img_Icon", AccessTools.all).GetValue(__instance);
            var textMeshProUGUI = (TextMeshProUGUI)__instance.GetType().GetField("txt_StoryName", AccessTools.all)
                .GetValue(__instance);
            if (books.Count < 0) return;
            image.enabled = true;
            image2.enabled = true;
            image2.sprite = ModParameters.ArtWorks["Light_Re21341"];
            image.sprite = ModParameters.ArtWorks["Light_Re21341"];
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ModName_Re21341")).Value
                .Name;
        }

        public static void UISettingInvenEquipPageListSlot_SetBooksData(UISettingInvenEquipPageListSlot __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            if (storyKey.workshopId != ModParameters.PackageId) return;
            var image = (Image)__instance.GetType().GetField("img_IconGlow", AccessTools.all).GetValue(__instance);
            var image2 = (Image)__instance.GetType().GetField("img_Icon", AccessTools.all).GetValue(__instance);
            var textMeshProUGUI = (TextMeshProUGUI)__instance.GetType().GetField("txt_StoryName", AccessTools.all)
                .GetValue(__instance);
            if (books.Count < 0) return;
            image.enabled = true;
            image2.enabled = true;
            image2.sprite = ModParameters.ArtWorks["Light_Re21341"];
            image.sprite = ModParameters.ArtWorks["Light_Re21341"];
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ModName_Re21341")).Value
                .Name;
        }

        public static void UISpriteDataManager_GetStoryIcon(UISpriteDataManager __instance,
            ref UIIconManager.IconSet __result, string story)
        {
            if (!ModParameters.ArtWorks.ContainsKey(story)) return;
            __result = new UIIconManager.IconSet
            {
                type = story,
                icon = ModParameters.ArtWorks[story],
                iconGlow = ModParameters.ArtWorks[story]
            };
        }

        public static bool BattleUnitView_ChangeSkin(BattleUnitView __instance, string charName)
        {
            if (!ModParameters.SkinNames.Contains(charName)) return true;
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
            return false;
        }
    }
}