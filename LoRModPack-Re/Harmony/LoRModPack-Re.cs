﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BLL_Re21341.Models;
using HarmonyLib;
using LOR_DiceSystem;
using Roland_Re21341.Passives;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Util_Re21341;
using Workshop;

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
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            MapUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance);
            UnitUtil.ChangePassiveItem();
            LocalizeUtil.AddLocalize();
            LocalizeUtil.RemoveError();
        }

        public static void BookModel_GetThumbSprite(BookModel __instance, ref Sprite __result)
        {
            if (__instance.BookId.packageId != ModParameters.PackageId) return;
            switch (__instance.BookId.id)
            {
                case 10000001:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/243003");
                    return;
                case 10000007:
                    __result = ModParameters.ArtWorks["AngelaDefault_Re21341"];
                    return;
                default:
                    return;
            }
        }
        public static void BookModel_SetXmlInfo(BookModel __instance, BookXmlInfo ____classInfo,
            ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId == ModParameters.PackageId && ModParameters.KeypageWithOnlyCardsList.Keys.Contains(__instance.BookId.id))
                ____onlyCards.AddRange(ModParameters.KeypageWithOnlyCardsList
                    .FirstOrDefault(x => x.Key.Equals(__instance.BookId.id)).Value.Select(id => ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id))));
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
            textMeshProUGUI.text = "Kamiyo's Mod Pack";
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
            textMeshProUGUI.text = "Kamiyo's Mod Pack";
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
            switch (charName)
            {
                case "BlackSilence3" when __instance.model.passiveDetail.HasPassive<PassiveAbility_BlackSilenceEgoMask_Re21341>():
                    __instance.model.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { charName };
                    return true;
                case "BlackSilence3":
                    return true;
            }
            var skinInfo =
                typeof(BattleUnitView).GetField("_skinInfo", AccessTools.all)?.GetValue(__instance) as
                    BattleUnitView.SkinInfo;
            skinInfo.state = BattleUnitView.SkinState.Default;
            skinInfo.skinName = charName;
            var currentMotionDetail = __instance.charAppearance.GetCurrentMotionDetail();
            __instance.DestroySkin();
            var gameObject =
                UnityEngine.Object.Instantiate(Singleton<AssetBundleManagerRemake>.Instance.LoadCharacterPrefab(charName, "", out var resourceName), __instance.model.view.characterRotationCenter);
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