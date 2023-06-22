using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.OldSamurai_Re21341.MapManager;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.OldSamurai_Re21341.Passives
{
    public class PassiveAbility_OldSamuraiEnemyDesc_Re21341 : PassiveAbilityBase
    {
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly bool _mapActive = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 6;
        public bool CreatureFilter;

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "OldSamurai_Re21341MapManager", Stage = "OldSamurai_Re21341", OneTurnEgo = false, Bgy = 0.2f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 1, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public string MapName = "OldSamurai_Re21341";
        public string MusicFileName = "Hornet_Re21341.ogg";
        public int Phase;
        public bool Revived;
        public string SaveDataId = "OldSamuraiSave21341";

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override void OnWaveStart()
        {
            if (_mapActive)
            {
                MapUtil.InitEnemyMap<OldSamurai_Re21341MapManager>(_cmh, MapModel);
                _cmh.EnforceMap();
            }

            Phase = NpcMechUtil.RestartPhase(SaveDataId);
            if (Phase != 0) ChangePhase(Phase);
        }

        public override void OnRoundStart()
        {
            if (_mapActive) _cmh.EnforceMap();
            owner.RemoveImmortalBuff();
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead() || Phase > 0) return;
            owner.ReviveCheck(ref Revived, owner.MaxHp, true);
            Phase++;
            ChangePhase(Phase);
        }

        public override void OnBattleEnd()
        {
            owner.OnEndBattleSave(SaveDataId, Phase);
        }

        public override void OnRoundStartAfter()
        {
            MapUtil.ActiveCreatureBattleCamFilterComponent(CreatureFilter);
        }

        private void ChangePhase(int phase)
        {
            switch (phase)
            {
                case 1:
                    var unitModel = new UnitModelRoot
                    {
                        PackageId = KamiyoModParameters.PackageId,
                        Id = 2,
                        UnitNameId = 2,
                        LockedEmotion = true
                    };
                    for (var i = 0; i < 3; i++)
                        UnitUtil.AddNewUnitWithDefaultData(unitModel,
                            BattleObjectManager.instance.GetList(owner.faction).Count, unitSide: owner.faction);
                    if (!_mapActive) break;
                    _cmh.SetMapBgm(MusicFileName, true, MapName);
                    CreatureFilter = true;
                    break;
            }
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x.passiveDetail.HasPassive<PassiveAbility_GhostSamurai_Re21341>()))
                unit.Die();
        }
    }
}