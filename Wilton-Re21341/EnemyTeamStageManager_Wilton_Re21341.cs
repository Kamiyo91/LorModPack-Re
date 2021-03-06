using System.Linq;
using BLL_Re21341.Models;
using CustomMapUtility;
using Hayate_Re21341.Passives;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using Util_Re21341.CommonBuffs;

namespace Wilton_Re21341
{
    public class EnemyTeamStageManager_Wilton_Re21341 : EnemyTeamStageManager
    {
        private bool _checkEnd;
        private bool _finalMech;
        private BattleUnitModel _mainEnemyModel;
        private Wilton_Re21341MapManager _mapManager;
        private bool _phaseChanged;

        public override void OnWaveStart()
        {
            _finalMech = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 6;
            CustomMapHandler.InitCustomMap("Wilton_Re21341", typeof(Wilton_Re21341MapManager), false, true, 0.5f, 0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            if (_finalMech)
                foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
                    unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Vip_Re21341());
            if (SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject is Wilton_Re21341MapManager)
                _mapManager = SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject as Wilton_Re21341MapManager;
            _phaseChanged = false;
            _checkEnd = false;
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
            HayateEntry();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }

        public override void OnRoundStart_After()
        {
            if (_phaseChanged) MapStaticUtil.ActiveCreatureBattleCamFilterComponent();
        }

        private void CheckPhase()
        {
            if (_mainEnemyModel.hp > 271 || _phaseChanged) return;
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
            var unit = UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = 6,
                Pos = 0,
                EmotionLevel = 5,
                AddEmotionPassive = false,
                OnWaveStart = true
            }, KamiyoModParameters.PackageId);
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