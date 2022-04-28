using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.Utils;
using Sound;
using Util_Re21341.Extentions;

namespace Mio_Re21341.MechUtil
{
    public class NpcMechUtil_Mio : NpcMechUtilBaseEx
    {
        private readonly NpcMechUtilBaseModel _model;

        public NpcMechUtil_Mio(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public void CheckPhase()
        {
            if (_model.Owner.hp > 271 || _model.Phase > 0) return;
            _model.Phase++;
            ForcedEgo();
            SetMassAttack(true);
            SetCounter(4);
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 4);
            SoundEffectPlayer.PlaySound("Creature/Angry_Meet");
        }

        public void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("PhaseMioRe21341", _model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction).Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>("PhaseMioRe21341", out var curPhase);
            _model.Phase = curPhase;
            if (_model.Phase < 1) return;
            ForcedEgo();
            SetMassAttack(true);
            SetCounter(4);
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 4);
            SoundEffectPlayer.PlaySound("Creature/Angry_Meet");
            _model.HasMechOnHp = false;
        }
    }
}