using System.Linq;
using BLL_Re21341.Models;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.BaseClass;
using KamiyoStaticUtil.Utils;

namespace OldSamurai_Re21341.MechUtil
{
    public class NpcMechUtil_OldSamurai : NpcMechUtilBase
    {
        private readonly NpcMechUtilBaseModel _model;

        public NpcMechUtil_OldSamurai(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public void CheckPhase()
        {
            if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count > 0 || _model.Phase > 0) return;
            _model.Phase++;
            UnitUtil.UnitReviveAndRecovery(_model.Owner, _model.Owner.MaxHp, true);
            ForcedEgo();
        }

        public void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("PhaseOldSamuraiRe21341", _model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                .Where(x => x.Book.BookId != new LorId(KamiyoModParameters.PackageId, 2)).Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>("PhaseOldSamuraiRe21341", out var curPhase);
            _model.Phase = curPhase;
            if (_model.Phase > 0) ForcedEgo();
        }
    }
}