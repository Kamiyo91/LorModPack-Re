using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Wilton_Re21341.MechUtil
{
    public class NpcMechUtil_Wilton : NpcMechUtilBase
    {
        private readonly NpcMechUtilBaseModel _model;

        public NpcMechUtil_Wilton(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public void CheckPhase()
        {
            if (_model.Owner.hp > 271 || _model.Phase > 0) return;
            _model.Phase++;
            ForcedEgo();
            SetMassAttack(true);
            SetCounter(5);
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 4);
        }

        public void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("Phase", _model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                .Where(x => x.Book.BookId != new LorId(ModParameters.PackageId, 9)).Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>("Phase", out var curPhase);
            _model.Phase = curPhase;
            if (_model.Phase < 1) return;
            ForcedEgo();
            SetMassAttack(true);
            SetCounter(5);
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 4);
            _model.HasMechOnHp = false;
        }
    }
}