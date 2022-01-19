using System.Linq;
using BLL_Re21341.Models;
using CustomMapUtility;
using Mio_Re21341.Buffs;
using Mio_Re21341.Passives;
using Util_Re21341;

namespace Mio_Re21341
{
    public class EnemyTeamStageManager_Mio_Re21341 : EnemyTeamStageManager
    {
        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();

        private bool _allySummon;
        private BattleUnitModel _mainEnemyModel;
        private bool _phaseChanged;

        public override void OnWaveStart()
        {
            _allySummon = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 2;
            CustomMapHandler.InitCustomMap("Mio_Re21341", typeof(Mio_Re21341MapManager), false, true, 0.5f, 0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
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
            MapUtil.ActiveCreatureBattleCamFilterComponent();
            if (_allySummon) PrepareAllyUnit();
            CustomMapHandler.SetMapBgm("MioPhase2_Re21341.ogg", true, "Mio_Re21341");
        }

        private void PrepareAllyUnit()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
            {
                if (!(unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_230008) is PassiveAbility_230008
                        passiveLone)) continue;
                unit.passiveDetail.DestroyPassive(passiveLone);
                unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 56));
            }

            var playerUnitList = BattleObjectManager.instance.GetList(Faction.Player);
            var allyUnit = UnitUtil.AddNewUnitPlayerSide(_floor, new UnitModel
            {
                Id = 10000900,
                Name = ModParameters.NameTexts.FirstOrDefault(x => x.Key.Equals("3")).Value + "?",
                EmotionLevel = 4,
                Pos = playerUnitList.Count,
                Sephirah = _floor.Sephirah
            });
            var passive =
                allyUnit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_GodFragment_Re21341) as
                    PassiveAbility_GodFragment_Re21341;
            passive?.ForcedEgo();
            if (!allyUnit.passiveDetail.PassiveList.Exists(x => x is PassiveAbility_KurosawaEmblem_Re21341))
                allyUnit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 37));
            if (!allyUnit.bufListDetail.HasBuf<BattleUnitBuf_KurosawaEmblem_Re21341>())
                allyUnit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_KurosawaEmblem_Re21341());
            allyUnit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 57));
        }
    }
}