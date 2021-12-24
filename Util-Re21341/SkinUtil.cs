using System;
using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using HarmonyLib;
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
            characterMotion.motionSpriteSet.Clear();
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Body));
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Head));
            characterMotion.motionSpriteSet.Add(new SpriteSet(
                characterMotion.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>(),
                CharacterAppearanceType.Body));
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
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ModName_Re21341")).Value
                .Name;
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
    }
}