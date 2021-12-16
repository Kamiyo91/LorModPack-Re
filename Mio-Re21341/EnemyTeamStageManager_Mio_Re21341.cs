using System.Linq;
using BLL_Re21341.Models;
using Mio_Re21341.Passives;
using Sound;
using Util_Re21341;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Mio_Re21341
{
    public class EnemyTeamStageManager_Mio_Re21341 : EnemyTeamStageManager
    {
        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();

        private BattleUnitModel _mainEnemyModel;
        private PassiveAbility_GodFragmentEnemy_Re21341 _mioEnemyPassive;
        private bool _phaseChanged;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Mio_Re21341", new Mio_Re21341MapManager(), false, true, 0.5f, 0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            if (_mainEnemyModel != null)
                _mioEnemyPassive =
                    _mainEnemyModel.passiveDetail.PassiveList.Find(x => x is PassiveAbility_GodFragmentEnemy_Re21341) as
                        PassiveAbility_GodFragmentEnemy_Re21341;
            _phaseChanged = false;
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
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
            _mioEnemyPassive.ForcedEgo();
            _mioEnemyPassive.ActiveMassAttackCount();
            _mioEnemyPassive.SetCountToMax();
            MapUtil.ActiveCreatureBattleCamFilterComponent();
            UnitUtil.ChangeCardCostByValue(_mainEnemyModel, -2, 4);
            SoundEffectPlayer.PlaySound("Creature/Angry_Meet");
            PrepareAllyUnit();
            CustomMapHandler.SetMapBgm("MioPhase2_Re21341.mp3", true, "Mio_Re21341");
        }

        private void PrepareAllyUnit()
        {
            var playerUnitList = BattleObjectManager.instance.GetList(Faction.Player);
            var allyUnit = UnitUtil.AddNewUnitPlayerSide(_floor, new UnitModel
            {
                Id = 10000003,
                Name = ModParameters.NameTexts.FirstOrDefault(x => x.Key.Equals("3")).Value + "?",
                EmotionLevel = 4,
                Pos = playerUnitList.Count,
                Sephirah = _floor.Sephirah
            });
            var passive =
                allyUnit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_GodFragment_Re21341) as
                    PassiveAbility_GodFragment_Re21341;
            passive?.ForcedEgo();
        }
    }
}