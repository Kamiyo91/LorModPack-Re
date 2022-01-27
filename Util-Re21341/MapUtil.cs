﻿using System.Collections.Generic;
using System.IO;
using BLL_Re21341.Models;
using CustomMapUtility;
using HarmonyLib;
using UnityEngine;

namespace Util_Re21341
{
    public static class MapUtil
    {
        public static void ChangeMap(MapModel model, Faction faction = Faction.Player)
        {
            if (CheckStageMap(model.StageIds) || SingletonBehavior<BattleSceneRoot>
                    .Instance.currentMapObject.isEgo ||
                Singleton<StageController>.Instance.GetStageModel().ClassInfo.stageType == StageType.Creature) return;
            CustomMapHandler.InitCustomMap(model.Stage, model.Component, model.IsPlayer, model.InitBgm, model.Bgx,
                model.Bgy, model.Fx, model.Fy);
            if (model.IsPlayer && !model.OneTurnEgo)
            {
                CustomMapHandler.ChangeToCustomEgoMapByAssimilation(model.Stage, faction);
                return;
            }

            CustomMapHandler.ChangeToCustomEgoMap(model.Stage, faction);
            MapChangedValue(true);
        }

        public static void ActiveCreatureBattleCamFilterComponent(bool value = true)
        {
            var battleCamera = (Camera)typeof(BattleCamManager).GetField("_effectCam",
                AccessTools.all)?.GetValue(SingletonBehavior<BattleCamManager>.Instance);
            if (!(battleCamera is null)) battleCamera.GetComponent<CameraFilterPack_Drawing_Paper3>().enabled = value;
        }

        public static bool CheckStageMap(List<int> ids)
        {
            ids.Add(12);
            return Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId ==
                   ModParameters.PackageId &&
                   ids.Contains(Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id);
        }

        private static void RemoveValueInAddedMap(string name, bool removeAll = false)
        {
            var mapList = (List<MapManager>)typeof(BattleSceneRoot).GetField("_addedMapList",
                AccessTools.all)?.GetValue(SingletonBehavior<BattleSceneRoot>.Instance);
            if (removeAll)
                mapList?.Clear();
            else
                mapList?.RemoveAll(x => x.name.Contains(name));
        }

        public static void ReturnFromEgoMap(string mapName, List<int> ids, bool isAssimilationMap = false)
        {
            if (CheckStageMap(ids) || Singleton<StageController>.Instance.GetStageModel().ClassInfo.stageType ==
                StageType.Creature) return;
            CustomMapHandler.RemoveCustomEgoMapByAssimilation(mapName);
            RemoveValueInAddedMap(mapName);
            if (isAssimilationMap) MapChangedValue(true);
            Singleton<StageController>.Instance.CheckMapChange();
            SingletonBehavior<BattleSoundManager>.Instance.SetEnemyTheme(SingletonBehavior<BattleSceneRoot>
                .Instance.currentMapObject.mapBgm);
            SingletonBehavior<BattleSoundManager>.Instance.CheckTheme();
        }

        private static void MapChangedValue(bool value)
        {
            typeof(StageController).GetField("_mapChanged", AccessTools.all)
                ?.SetValue(Singleton<StageController>.Instance, value);
        }

        public static void GetArtWorks(DirectoryInfo dir)
        {
            if (dir.GetDirectories().Length != 0)
            {
                var directories = dir.GetDirectories();
                foreach (var t in directories) GetArtWorks(t);
            }

            foreach (var fileInfo in dir.GetFiles())
            {
                var texture2D = new Texture2D(2, 2);
                texture2D.LoadImage(File.ReadAllBytes(fileInfo.FullName));
                var value = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
                    new Vector2(0f, 0f));
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                ModParameters.ArtWorks[fileNameWithoutExtension] = value;
            }
        }
    }
}