using System.Collections.Generic;
using System.Linq;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;
using BigDLL4221.Utils;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Hayate_Re21341.Passives;
using KamiyoModPack.Util_Re21341.CommonBuffs;

namespace KamiyoModPack.Wilton_Re21341
{
    public class EnemyTeamStageManager_Wilton_Re21341 : EnemyTeamStageManager_BaseWithCMUOnly_DLL4221
    {
        private bool _checkEnd;
        private bool _finalMech;
        private BattleUnitModel _mainEnemyModel;
        private Wilton_Re21341MapManager _mapManager;
        private bool _phaseChanged;

        public override void OnWaveStart()
        {
            SetParameters(CustomMapHandler.GetCMU(KamiyoModParameters.PackageId),
                new List<MapModel> { KamiyoModParameters.WiltonMap });
            base.OnWaveStart();
            _finalMech = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 6;
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            //if (_finalMech)
            //    foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
            //        unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Vip_Re21341());
            _phaseChanged = false;
            _checkEnd = false;
        }
        
        public override void OnRoundEndTheLast()
        {
            CheckMapManager();
            CheckPhase();
            HayateEntry();
        }

        private void CheckMapManager()
        {
            if (_mapManager == null && SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject is Wilton_Re21341MapManager)
                _mapManager = SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject as Wilton_Re21341MapManager;
        }
        private void CheckPhase()
        {
            if (_mainEnemyModel.hp > 271 || _phaseChanged) return;
            ChangeFilterStatus(true);
            _phaseChanged = true;
            _mapManager.Phase = 1;
            _mapManager.Update();
        }

        private void HayateEntry()
        {
            if (!_finalMech || BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count >= 1 || _checkEnd) return;
            _checkEnd = true;
            foreach (var playerUnit in BattleObjectManager.instance.GetAliveList(Faction.Player))
                playerUnit.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_Vip_Re21341));
            var unit = UnitUtil.AddNewUnitWithDefaultData(KamiyoModParameters.HayateLastScene, 0,
                emotionLevel: 5, unitSide: Faction.Enemy);
            unit.allyCardDetail.ExhaustAllCards();
            unit.allyCardDetail.AddNewCard(new LorId(KamiyoModParameters.PackageId, 903));
            var passive =
                unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_HayateNpc_Re21341) as
                    PassiveAbility_HayateNpc_Re21341;
            passive?.SetWiltonCaseOn();
            unit.moveDetail.ReturnToFormationByBlink(true);
            UnitUtil.RefreshCombatUI();
        }

        public override void OnEndBattle()
        {
            if (_finalMech)
                BattleObjectManager.instance.GetList(Faction.Player)
                    .FirstOrDefault(x => x.UnitData.unitData.bookItem.ClassInfo.id.id == 10000901)?.Revive(1);
        }
    }
}