using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using CustomMapUtility;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using OldSamurai_Re21341.MapManager;
using Util_Re21341;

namespace OldSamurai_Re21341.Buffs
{
    public class BattleUnitBuf_OldSamuraiEgoNpc_Re21341 : BattleUnitBuf
    {
        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();

        public override bool isAssimilation => true;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            ChangeToSamuraiEgoMap();
            for (var i = 1; i < 4; i++)
                if (owner.faction == Faction.Enemy)
                    UnitUtil.AddNewUnitEnemySide(new UnitModel
                    {
                        Id = 2,
                        Pos = BattleObjectManager.instance.GetAliveList(owner.faction).Count,
                        LockedEmotion = true,
                        OnWaveStart = true
                    }, KamiyoModParameters.PackageId);
                else
                    //var indexList = UnitUtil.GetSamuraiGhostIndex(owner.index);
                    //foreach (var unit in BattleObjectManager.instance.GetList(Faction.Player)
                    //             .Where(x => indexList.Contains(x.index)))
                    //BattleObjectManager.instance.UnregisterUnit(unit);
                    for (var i1 = 0; i1 < 3; i1++)
                        UnitUtil.AddNewUnitPlayerSide(_floor, new UnitModel
                        {
                            Id = 2,
                            Name = ModParameters.NameTexts
                                .FirstOrDefault(x => x.Key.Equals(new LorId(KamiyoModParameters.PackageId, 2))).Value,
                            Pos = BattleObjectManager.instance.GetAliveList(owner.faction).Count,
                            LockedEmotion = true,
                            Sephirah = _floor.Sephirah
                        }, KamiyoModParameters.PackageId);

            UnitUtil.RefreshCombatUI();
        }

        public override void OnRoundStart()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id !=
                new LorId(KamiyoModParameters.PackageId, 1)) CustomMapHandler.EnforceTheme();
            if (BattleObjectManager.instance.GetAliveList(_owner.faction).Count <= 1) RemoveSamuraiEgoMap();
        }

        private static void ChangeToSamuraiEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "OldSamurai_Re21341",
                StageIds = new List<LorId>
                    { new LorId(KamiyoModParameters.PackageId, 1), new LorId(KamiyoModParameters.PackageId, 12) },
                IsPlayer = true,
                Component = typeof(OldSamuraiPlayer_Re21341MapManager),
                Bgy = 0.2f
            });
        }

        private void RemoveSamuraiEgoMap()
        {
            Destroy();
            MapUtil.ReturnFromEgoMap("OldSamurai_Re21341",
                new List<LorId>
                    { new LorId(KamiyoModParameters.PackageId, 1), new LorId(KamiyoModParameters.PackageId, 12) },
                true);
        }
    }
}