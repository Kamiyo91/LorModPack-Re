using System.Linq;
using BLL_Re21341.Models;
using CustomMapUtility;
using Util_Re21341;

namespace OldSamurai_Re21341.Buffs
{
    public class BattleUnitBuf_OldSamuraiEgo_Re21341 : BattleUnitBuf
    {
        public override bool isAssimilation => true;
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            var currentStageFloorModel = Singleton<StageController>.Instance.GetCurrentStageFloorModel();
            var floor = Singleton<StageController>.Instance.GetStageModel().GetFloor(currentStageFloorModel.Sephirah);
            if (owner.faction == Faction.Player)
            {
                ChangeToSamuraiEgoMap();
                CustomMapHandler.SetEnemyTheme("Hornet.mp3");
            }
            var id = owner.faction == Faction.Player ? 10000003 : 3;
            var indexList = UnitUtil.GetSamuraiGhostIndex(owner.index);
            foreach (var unit in BattleObjectManager.instance.GetList(Faction.Player)
                         .Where(x => indexList.Contains(x.index))) BattleObjectManager.instance.UnregisterUnit(unit);
            for (var i = 0; i < 3; i++)
                UnitUtil.AddNewUnitPlayerSide(floor, new UnitModel
                {
                    Id = id,
                    Name = "Samurai's Ghost",
                    Pos = indexList[i],
                    LockedEmotion = true,
                    Sephirah = floor.Sephirah
                });
            UnitUtil.RefreshCombatUI();
        }

        public override void OnRoundStart()
        {
            if (_owner.faction == Faction.Player && BattleObjectManager.instance.GetAliveList(Faction.Player).Count <= 1)
            {
                RemoveSamuraiEgoMap();
            }
        }

        private static void ChangeToSamuraiEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "OldSamurai",
                StageId = 1,
                IsPlayer = true,
                Component = new OldSamurai_Re21341MapManager(),
                Bgy = 0.2f
            });
        }
        private void RemoveSamuraiEgoMap()
        {
            MapUtil.RemoveValueInEgoMap("OldSamurai");
            MapUtil.ReturnFromEgoMap("OldSamurai", _owner, 1);
            SingletonBehavior<BattleSoundManager>.Instance.SetEnemyTheme(SingletonBehavior<BattleSceneRoot>
                .Instance.currentMapObject.mapBgm);
            SingletonBehavior<BattleSoundManager>.Instance.CheckTheme();
            Destroy();
        }
    }
}
