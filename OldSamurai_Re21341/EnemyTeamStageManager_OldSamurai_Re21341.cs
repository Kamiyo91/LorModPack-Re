﻿using System.Linq;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using OldSamurai_Re21341.Buffs;
using UnityEngine;
using Util_Re21341;
using Util_Re21341.BaseClass;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace OldSamurai_Re21341
{
    public class EnemyTeamStageManager_OldSamurai_Re21341 : EnemyTeamStageManager
    {
        private Task _changeBgm;
        private BattleUnitModel _mainEnemyModel;
        private bool _phaseChanged;
        private MechUtilBase _mechUtil;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("OldSamurai", new OldSamurai_Re21341MapManager(), false, true, 0.5f, 0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            _mechUtil = new MechUtilBase(new MechUtilBaseModel
            {
                Owner = _mainEnemyModel,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_OldSamuraiEgo_Re21341),
                EgoCardId = new LorId(ModParameters.PackageId, 8)
            });
            _phaseChanged = false;
            Debug.LogError("End");
        }

        public override void OnRoundEndTheLast() => CheckPhase();

        public override void OnRoundStart()
        {
            //if(_mechUtil.EgoCheck()) _mechUtil.EgoActive();
            CustomMapHandler.EnforceMap();
            MapUtil.CheckAndChangeBgm(ref _changeBgm);
        }
        private void CheckPhase()
        {
            if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count > 0 || _phaseChanged) return;
            Debug.LogError("Entry");
            _phaseChanged = true;
            UnitUtil.UnitReviveAndRecovery(_mainEnemyModel,_mainEnemyModel.MaxHp);
            _mechUtil.ForcedEgo();
            MapUtil.PrepareChangeBgm("Hornet.mp3", ref _changeBgm);
        }
    }
}
