using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using CustomMapUtility;
using Hayate_Re21341;
using Kamiyo_Re21341.MapManager;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using Mio_Re21341;
using OldSamurai_Re21341.MapManager;
using Raziel_Re21341;
using Wilton_Re21341;

namespace BossRush_Re21341
{
    public class EnemyTeamStageManager_BossRush_Re21341 : EnemyTeamStageManager
    {
        private readonly List<int> _filterPhases = new List<int> { 3, 7, 9 };
        private int _mapInfo;
        private int _phase;
        private List<bool> _phaseChanged = new List<bool>();

        public override void OnWaveStart()
        {
            _phase = 0;
            _mapInfo = 0;
            if (Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>("PhaseRushRe21341", out var phase)) _phase = phase;
            if (Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>("MapInfoRushRe21341", out var mapInfo))
                _mapInfo = mapInfo;
            if (Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<List<bool>>("PhaseChangedRushRe21341", out var phaseChanged))
                _phaseChanged = phaseChanged;
            else
                for (var i = 0; i < 10; i++)
                    _phaseChanged.Add(false);
            CustomMapHandler.InitCustomMap("OldSamurai_Re21341", typeof(OldSamurai_Re21341MapManager), false, true,
                0.5f, 0.2f);
            CustomMapHandler.InitCustomMap("Mio_Re21341", typeof(Mio_Re21341MapManager), false, true, 0.5f, 0.2f);
            CustomMapHandler.InitCustomMap("Kamiyo1_Re21341", typeof(Kamiyo1_Re21341MapManager), false, true, 0.5f,
                0.2f, 0.5f, 0.45f);
            CustomMapHandler.InitCustomMap("Kamiyo2_Re21341", typeof(Kamiyo2_Re21341MapManager), false, true, 0.5f,
                0.475f, 0.5f, 0.225f);
            CustomMapHandler.InitCustomMap("Hayate_Re21341", typeof(Hayate_Re21341MapManager), false, true, 0.5f, 0.3f,
                0.5f, 0.475f);
            CustomMapHandler.InitCustomMap("Wilton_Re21341", typeof(Wilton_Re21341MapManager), false, true, 0.5f, 0.2f);
            CustomMapHandler.InitCustomMap("Raziel_Re21341", typeof(Raziel_Re21341MapManager), false, true, 0.5f,
                0.375f, 0.5f, 0.225f);
            CustomMapHandler.EnforceMap(_mapInfo);
            Singleton<StageController>.Instance.CheckMapChange();
            switch (_phase)
            {
                case 1:
                    CustomMapHandler.SetMapBgm("Hornet_Re21341.ogg", true, "OldSamurai_Re21341");
                    break;
                case 3:
                    CustomMapHandler.SetMapBgm("MioPhase2_Re21341.ogg", true, "Mio_Re21341");
                    break;
                case 7:
                    CustomMapHandler.SetMapBgm("HayatePhase2_Re21341.ogg", true, "Hayate_Re21341");
                    break;
                case 9:
                    if (SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject is Wilton_Re21341MapManager)
                    {
                        var wiltonMapManager =
                            SingletonBehavior<BattleSceneRoot>.Instance
                                .currentMapObject as Wilton_Re21341MapManager;
                        if (wiltonMapManager != null)
                        {
                            wiltonMapManager.Phase = 1;
                            wiltonMapManager.Update();
                        }
                    }

                    break;
            }
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap(_mapInfo);
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
        }

        public override void OnRoundStart_After()
        {
            if (_filterPhases.Contains(_phase)) MapStaticUtil.ActiveCreatureBattleCamFilterComponent();
        }

        private void CheckPhase()
        {
            switch (_phase)
            {
                case 0:
                    if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count < 1 && !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        CustomMapHandler.SetMapBgm("Hornet_Re21341.ogg", true, "OldSamurai_Re21341");
                    }

                    break;
                case 1:
                    if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count < 1 && !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        _mapInfo++;
                        foreach (var unit in BattleObjectManager.instance.GetList(Faction.Enemy))
                            BattleObjectManager.instance.UnregisterUnit(unit);
                        AddEnemyUnit(3, 3);
                        CustomMapHandler.EnforceMap(_mapInfo);
                        Singleton<StageController>.Instance.CheckMapChange();
                    }

                    break;
                case 2:
                    if (BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault()?.hp < 272 &&
                        !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        CustomMapHandler.SetMapBgm("MioPhase2_Re21341.ogg", true, "Mio_Re21341");
                    }

                    break;
                case 3:
                    if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count < 1 && !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        _mapInfo++;
                        foreach (var unit in BattleObjectManager.instance.GetList(Faction.Enemy))
                            BattleObjectManager.instance.UnregisterUnit(unit);
                        AddEnemyUnit(4, 4);
                        MapStaticUtil.ActiveCreatureBattleCamFilterComponent(false);
                        CustomMapHandler.EnforceMap(_mapInfo);
                        Singleton<StageController>.Instance.CheckMapChange();
                    }

                    break;
                case 4:
                    if (BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault()?.hp < 162 &&
                        !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        _mapInfo++;
                        CustomMapHandler.EnforceMap(_mapInfo);
                        Singleton<StageController>.Instance.CheckMapChange();
                    }

                    break;
                case 5:
                    if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count < 1 && !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        _mapInfo++;
                        foreach (var unit in BattleObjectManager.instance.GetList(Faction.Enemy))
                            BattleObjectManager.instance.UnregisterUnit(unit);
                        AddEnemyUnit(6, 5);
                        CustomMapHandler.EnforceMap(_mapInfo);
                        Singleton<StageController>.Instance.CheckMapChange();
                    }

                    break;
                case 6:
                    if (BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault()?.hp < 528 &&
                        !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        CustomMapHandler.SetMapBgm("HayatePhase2_Re21341.ogg", true, "Hayate_Re21341");
                    }

                    break;
                case 7:
                    if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count < 1 && !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        _mapInfo++;
                        foreach (var unit in BattleObjectManager.instance.GetList(Faction.Enemy))
                            BattleObjectManager.instance.UnregisterUnit(unit);
                        AddEnemyUnit(8, 5);
                        MapStaticUtil.ActiveCreatureBattleCamFilterComponent(false);
                        CustomMapHandler.EnforceMap(_mapInfo);
                        Singleton<StageController>.Instance.CheckMapChange();
                    }

                    break;
                case 8:
                    if (BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault()?.hp < 272 &&
                        !_phaseChanged[_phase])
                    {
                        if (SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject is Wilton_Re21341MapManager)
                        {
                            var wiltonMapManager =
                                SingletonBehavior<BattleSceneRoot>.Instance
                                    .currentMapObject as Wilton_Re21341MapManager;
                            if (wiltonMapManager != null)
                            {
                                wiltonMapManager.Phase = 1;
                                wiltonMapManager.Update();
                            }
                        }

                        _phaseChanged[_phase] = true;
                        _phase++;
                    }

                    break;
                case 9:
                    if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count < 1 && !_phaseChanged[_phase])
                    {
                        _phaseChanged[_phase] = true;
                        _phase++;
                        _mapInfo++;
                        foreach (var unit in BattleObjectManager.instance.GetList(Faction.Enemy))
                            BattleObjectManager.instance.UnregisterUnit(unit);
                        AddEnemyUnit(10, 5);
                        MapStaticUtil.ActiveCreatureBattleCamFilterComponent(false);
                        CustomMapHandler.EnforceMap(_mapInfo);
                        Singleton<StageController>.Instance.CheckMapChange();
                    }

                    break;
            }
        }

        private static void AddEnemyUnit(int id, int emotionLevel)
        {
            UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = id,
                EmotionLevel = emotionLevel,
                Pos = 0,
                OnWaveStart = true
            }, KamiyoModParameters.PackageId);
            UnitUtil.RefreshCombatUI();
        }

        public override void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("PhaseRushRe21341", _phase);
            stageModel.SetStageStorgeData("MapInfoRushRe21341", _mapInfo);
            stageModel.SetStageStorgeData("PhaseChangedRushRe21341", _phaseChanged);
        }
    }
}