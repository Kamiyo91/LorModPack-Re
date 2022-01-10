﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using HarmonyLib;
using Sound;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Workshop;
using Object = UnityEngine.Object;

namespace Util_Re21341
{
    public static class SkinUtil
    {
        public static CharacterMotion CopyCharacterMotion(CharacterAppearance apprearance, ActionDetail detail)
        {
            var characterMotion = Object.Instantiate(apprearance._motionList[0]);
            characterMotion.transform.parent = apprearance._motionList[0].transform.parent;
            characterMotion.transform.name = apprearance._motionList[0].transform.name;
            characterMotion.actionDetail = detail;
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Body));
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Head));
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Body));
            characterMotion.transform.localScale = new Vector3(1, 1, 1);
            return characterMotion;
        }

        public static void LoadBookSkinsExtra()
        {
            try
            {
                var dictionary =
                    typeof(CustomizingBookSkinLoader).GetField("_bookSkinData", AccessTools.all)
                            ?.GetValue(Singleton<CustomizingBookSkinLoader>.Instance) as
                        Dictionary<string, List<WorkshopSkinData>>;
                foreach (var item in ModParameters.SkinParameters)
                {
                    var workshopSkinData =
                        dictionary?[ModParameters.PackageId].Find(x => x.dataName.Contains(item.Name));
                    var clothCustomizeData = workshopSkinData.dic[ActionDetail.Default];
                    foreach (var skinData in item.SkinParameters.Where(x =>
                                 !workshopSkinData.dic.ContainsKey(x.Motion)))
                    {
                        var value = new ClothCustomizeData
                        {
                            spritePath = clothCustomizeData.spritePath.Replace("Default.png", skinData.FileName),
                            frontSpritePath = clothCustomizeData.spritePath.Replace("Default.png", skinData.FileName),
                            hasFrontSprite = clothCustomizeData.hasFrontSprite,
                            pivotPos = new Vector2((skinData.PivotPosX + 512f) / 1024f,
                                (skinData.PivotPosY + 512f) / 1024f),
                            headPos = new Vector2(skinData.PivotHeadX / 100f, skinData.PivotHeadY / 100f),
                            headRotation = skinData.HeadRotation,
                            direction = CharacterMotion.MotionDirection.FrontView,
                            headEnabled = clothCustomizeData.headEnabled,
                            hasFrontSpriteFile = clothCustomizeData.hasFrontSpriteFile,
                            hasSpriteFile = clothCustomizeData.hasSpriteFile
                        };
                        workshopSkinData.dic.Add(skinData.Motion, value);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void SetBooksData(UIOriginEquipPageList instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            if (storyKey.workshopId != ModParameters.PackageId) return;
            var image = (Image)instance.GetType().GetField("img_IconGlow", AccessTools.all).GetValue(instance);
            var image2 = (Image)instance.GetType().GetField("img_Icon", AccessTools.all).GetValue(instance);
            var textMeshProUGUI = (TextMeshProUGUI)instance.GetType().GetField("txt_StoryName", AccessTools.all)
                .GetValue(instance);
            if (books.Count < 0) return;
            image.enabled = true;
            image2.enabled = true;
            image2.sprite = ModParameters.ArtWorks["Light_Re21341"];
            image.sprite = ModParameters.ArtWorks["Light_Re21341"];
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("CredenzaName_Re21341"))
                .Value
                .Name;
        }

        public static void SetEpisodeSlots(UIBookStoryChapterSlot instance, UIBookStoryPanel panel,
            List<UIBookStoryEpisodeSlot> episodeSlots)
        {
            if (instance.chapter != 7) return;
            var uibookStoryEpisodeSlot =
                episodeSlots.Find(x => x.books.Find(y => y.id.packageId == ModParameters.PackageId) != null);
            if (uibookStoryEpisodeSlot == null) return;
            var books = uibookStoryEpisodeSlot.books;
            uibookStoryEpisodeSlot.Init(
                panel.panel.GetChapterBooksData(instance.chapter).FindAll(x =>
                    x.id.packageId == ModParameters.PackageId && ModParameters.BooksIds.Contains(x.id.id)), instance);
            ((TextMeshProUGUI)uibookStoryEpisodeSlot.GetType().GetField("episodeText", AccessTools.all)
                .GetValue(uibookStoryEpisodeSlot)).text = ModParameters.EffectTexts
                .FirstOrDefault(x => x.Key.Equals("CredenzaName_Re21341")).Value
                .Name;
            var image = (Image)uibookStoryEpisodeSlot.GetType().GetField("episodeIconGlow", AccessTools.all)
                .GetValue(uibookStoryEpisodeSlot);
            var image2 = (Image)uibookStoryEpisodeSlot.GetType().GetField("episodeIcon", AccessTools.all)
                .GetValue(uibookStoryEpisodeSlot);
            image2.sprite = ModParameters.ArtWorks["Light_Re21341"];
            image.sprite = ModParameters.ArtWorks["Light_Re21341"];
            instance.InstatiateAdditionalSlot();
            var uibookStoryEpisodeSlot2 = episodeSlots[episodeSlots.Count - 1];
            books.RemoveAll(x => x.id.packageId == ModParameters.PackageId);
            uibookStoryEpisodeSlot2.Init(instance.chapter, books, instance);
        }

        public static void GetThumbSprite(LorId bookId, ref Sprite result)
        {
            if (bookId.packageId != ModParameters.PackageId) return;
            var sprite = ModParameters.SpritePreviewChange.FirstOrDefault(x => x.Value.Contains(bookId.id));
            if (!string.IsNullOrEmpty(sprite.Key) && sprite.Value.Any())
            {
                result = ModParameters.ArtWorks[sprite.Key];
                return;
            }

            var defaultSprite =
                ModParameters.DefaultSpritePreviewChange.FirstOrDefault(x => x.Value.Contains(bookId.id));
            if (!string.IsNullOrEmpty(defaultSprite.Key) && defaultSprite.Value.Any())
                result = Resources.Load<Sprite>(defaultSprite.Key);
        }

        public static void BurnEffect(BattleUnitModel owner)
        {
            var gameObject = Util.LoadPrefab("Battle/DiceAttackEffects/New/FX/DamageDebuff/FX_DamageDebuff_Fire");
            if (gameObject != null)
                if (owner?.view != null)
                {
                    gameObject.transform.parent = owner.view.camRotationFollower;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localScale = Vector3.one;
                    gameObject.transform.localRotation = Quaternion.identity;
                }

            SoundEffectPlayer.PlaySound("Buf/Effect_Burn");
        }

        public static void SakuraEffect(BattleUnitModel owner)
        {
            var object2 = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_ActivateParticle");
            if (object2 != null)
            {
                var gameObject2 = Object.Instantiate(object2) as GameObject;
                if (gameObject2 != null)
                {
                    gameObject2.transform.parent = owner.view.charAppearance.transform;
                    gameObject2.transform.localPosition = Vector3.zero;
                    gameObject2.transform.localRotation = Quaternion.identity;
                    gameObject2.transform.localScale = Vector3.one;
                }
            }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Buf/Effect_Index_Unlock");
        }

        public static void PreLoadBufIcons()
        {
            foreach (var baseGameIcon in Resources.LoadAll<Sprite>("Sprites/BufIconSheet/")
                         .Where(x => !BattleUnitBuf._bufIconDictionary.ContainsKey(x.name)))
                BattleUnitBuf._bufIconDictionary.Add(baseGameIcon.name, baseGameIcon);
            foreach (var artWork in ModParameters.ArtWorks.Where(x =>
                         !x.Key.Contains("Glow") && !x.Key.Contains("Default") &&
                         !BattleUnitBuf._bufIconDictionary.ContainsKey(x.Key)))
                BattleUnitBuf._bufIconDictionary.Add(artWork.Key, artWork.Value);
        }
    }
}