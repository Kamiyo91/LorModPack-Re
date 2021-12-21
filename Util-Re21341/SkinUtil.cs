using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BLL_Re21341.Models;
using HarmonyLib;
using Mod;
using UnityEngine;
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
                Debug.LogError("Exit Skin Load");
            }
            catch (Exception ex)
            {
                Singleton<ModContentManager>.Instance.AddErrorLog(ex.Message);
            }
        }
    }
}
