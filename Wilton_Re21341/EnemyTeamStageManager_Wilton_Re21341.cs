using System.Linq;
using BLL_Re21341.Models;
using Util_Re21341;
using Util_Re21341.CommonBuffs;
using Util_Re21341.CustomMapUtility.Assemblies;
using Wilton_Re21341.Passives;

namespace Wilton_Re21341
{
    public class EnemyTeamStageManager_Wilton_Re21341 : EnemyTeamStageManager
    {
        private BattleUnitModel _mainEnemyModel;
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
            _wiltonEnemyPassive.ForcedEgo();
            _wiltonEnemyPassive.ActiveMassAttackCount();
            _wiltonEnemyPassive.SetCountToMax();
            MapUtil.ActiveCreatureBattleCamFilterComponent();
            UnitUtil.ChangeCardCostByValue(_mainEnemyModel, -2, 4);
            CustomMapHandler.SetMapBgm("WiltonPhase2_Re21341.mp3", true, "Wilton_Re21341");
            SummonWillWisp();
        }

        private void SummonWillWisp()
        {
            for (var i = 1; i < 5; i++)
            {
                var unit = UnitUtil.AddNewUnitEnemySide(new UnitModel
                {
                    Id = 9,
                    Pos = i,
                    EmotionLevel = _mainEnemyModel.emotionDetail.EmotionLevel
                });
                unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_WillOWispAura_Re21341());
            }

            UnitUtil.RefreshCombatUI();
        }
    }
}