using System.Linq;
using BLL_Re21341.Models;
using Hayate_Re21341.Passives;
using Util_Re21341;
using Util_Re21341.CommonBuffs;
using Util_Re21341.CustomMapUtility.Assemblies;
using Wilton_Re21341.Passives;

namespace Wilton_Re21341
{
    public class EnemyTeamStageManager_Wilton_Re21341 : EnemyTeamStageManager
    {
        private bool _checkEnd;
        private BattleUnitModel _mainEnemyModel;
        private Wilton_Re21341MapManager _mapManager;
        private bool _phaseChanged;
        private PassiveAbility_KurosawaButlerEnemy_Re21341 _wiltonEnemyPassive;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Wilton_Re21341", new Wilton_Re21341MapManager(), false, true, 0.5f, 0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            if (_mainEnemyModel != null)
                _wiltonEnemyPassive =
                    _mainEnemyModel.passiveDetail.PassiveList.Find(x => x is PassiveAbility_KurosawaButlerEnemy_Re21341)
                        as
                        PassiveAbility_KurosawaButlerEnemy_Re21341;
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
            if (_phaseChanged) MapUtil.ActiveCreatureBattleCamFilterComponent();
        }

        private void CheckPhase()
        {
            if (_mainEnemyModel.hp > 271 || _phaseChanged) return;
            _phaseChanged = true;
            _wiltonEnemyPassive.ForcedEgo();
            _wiltonEnemyPassive.ActiveMassAttackCount();
            _wiltonEnemyPassive.SetCountToMax();
            UnitUtil.ChangeCardCostByValue(_mainEnemyModel, -2, 4);
            _mapManager.Phase = 1;
            _mapManager.Update();
        }

        private void HayateEntry()
        {
            if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count >= 1 || _checkEnd) return;
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
            });
            unit.allyCardDetail.ExhaustAllCards();
            unit.allyCardDetail.AddNewCard(new LorId(ModParameters.PackageId, 903));
            var passive =
                unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_HayateNpc_Re21341) as
                    PassiveAbility_HayateNpc_Re21341;
            passive?.SetWiltonCaseOn();
            UnitUtil.RefreshCombatUI();
        }
    }
}