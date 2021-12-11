using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using HarmonyLib;
using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Util_Re21341
{
    public static class MapUtil
    {
        public static void ChangeMap(MapModel model, Faction faction = Faction.Player)
        {
            if (CheckStageMap(model.StageId)) return;
            CustomMapHandler.InitCustomMap(model.Stage, model.Component, model.IsPlayer, model.InitBgm, model.Bgx,
                model.Bgy, model.Fx, model.Fy);
            if (model.IsPlayer && !model.OneTurnEgo)
            {
                CustomMapHandler.ChangeToCustomEgoMapByAssimilation(model.Stage, faction);
                return;
            }
            CustomMapHandler.ChangeToCustomEgoMap(model.Stage, faction);
        }
        public static void ActiveCreatureBattleCamFilterComponent()
        {
            var battleCamera = (Camera)typeof(BattleCamManager).GetField("_effectCam",
                AccessTools.all)?.GetValue(SingletonBehavior<BattleCamManager>.Instance);
            if (!(battleCamera is null)) battleCamera.GetComponent<CameraFilterPack_Drawing_Paper3>().enabled = true;
        }

        private static bool CheckStageMap(int id) =>
            Singleton<StageController>.Instance.GetStageModel().ClassInfo.id ==
            new LorId(ModParameters.PackageId, id);

        private static void RemoveValueInAddedMap(string name, bool removeAll = false)
        {
            var mapList = (List<MapManager>)typeof(BattleSceneRoot).GetField("_addedMapList",
                AccessTools.all)?.GetValue(SingletonBehavior<BattleSceneRoot>.Instance);
            if (removeAll)
                mapList?.Clear();
            else
                mapList?.RemoveAll(x => x.name.Contains(name));
        }

        public static void PrepareChangeBgm(string bgmName, ref Task changeBgm)
        {
            changeBgm = Task.Run(() =>
            {
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.mapBgm =
                    CustomMapHandler.CustomBgmParse(new[]
                    {
                        bgmName
                    });
            });
        }

        public static void CheckAndChangeBgm(ref Task changeBgm)
        {
            if (changeBgm == null) return;
            changeBgm.Wait();
            SingletonBehavior<BattleSoundManager>.Instance.SetEnemyTheme(SingletonBehavior<BattleSceneRoot>.Instance
                .currentMapObject.mapBgm);
            changeBgm = null;
        }

        public static void ReturnFromEgoMap(string mapName, SephirahType sephirah,int id)
        {
            if (CheckStageMap(id)) return;
            CustomMapHandler.RemoveCustomEgoMapByAssimilation(mapName);
            if (SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.sephirahType == sephirah)
                SingletonBehavior<BattleSoundManager>.Instance.OnStageStart();
            SingletonBehavior<BattleSoundManager>.Instance.CheckTheme();
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
