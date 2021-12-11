using System.Linq;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using OldSamurai_Re21341.MapManager;
using Util_Re21341;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace OldSamurai_Re21341.Buffs
{
    public class BattleUnitBuf_OldSamuraiEgo_Re21341 : BattleUnitBuf
    {
        private readonly SephirahType _sephirah = Singleton<StageController>.Instance.GetCurrentStageFloorModel().Sephirah;
        public override bool isAssimilation => true;
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            var floor = Singleton<StageController>.Instance.GetStageModel().GetFloor(_sephirah);
            ChangeToSamuraiEgoMap();
            var indexList = UnitUtil.GetSamuraiGhostIndex(owner.index);
            foreach (var unit in BattleObjectManager.instance.GetList(Faction.Player)
                         .Where(x => indexList.Contains(x.index))) BattleObjectManager.instance.UnregisterUnit(unit);
            for (var i = 0; i < 3; i++)
                UnitUtil.AddNewUnitPlayerSide(floor, new UnitModel
                {
                    Id = 10000002,
                    Name = "Samurai's Ghost",
                    Pos = indexList[i],
                    LockedEmotion = true,
                    Sephirah = floor.Sephirah
                });
            UnitUtil.RefreshCombatUI();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceTheme();
            if (BattleObjectManager.instance.GetAliveList(Faction.Player).Count <= 1)
            {
                RemoveSamuraiEgoMap();
            }
        }

        private static void ChangeToSamuraiEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "OldSamurai_Re21341",
                StageId = 1,
                IsPlayer = true,
                Component = new OldSamuraiPlayer_Re21341MapManager(),
                Bgy = 0.2f
            });
        }
        private void RemoveSamuraiEgoMap()
        {
            Destroy();
            MapUtil.ReturnFromEgoMap("OldSamurai_Re21341", _sephirah,1);
        }
    }
}
