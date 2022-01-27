using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using CustomMapUtility;
using LOR_XML;
using Raziel_Re21341.Buffs;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Raziel_Re21341.MechUtil
{
    public class NpcMechUtil_Raziel : NpcMechUtilBase
    {
        private readonly NpcMechUtilBaseModel _model;

        public NpcMechUtil_Raziel(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public void IncreasePhase()
        {
            _model.Phase++;
        }

        public void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("PhaseRazielRe21341", _model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction).Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>("PhaseRazielRe21341", out var curPhase);
            _model.Phase = curPhase;
            if (_model.Phase < 2) return;
            ForcedEgo();
            _model.Owner.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 41));
        }

        public void CheckPhase()
        {
            if (_model.Phase > 1 && !_model.Owner.bufListDetail.HasBuf<BattleUnitBuf_OwlSpiritNpc_Re21341>())
            {
                ForcedEgo();
                _model.Owner.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 41));
                if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id ==
                    new LorId(ModParameters.PackageId, 7) || Singleton<StageController>.Instance.GetStageModel().ClassInfo.id ==
                    new LorId(ModParameters.PackageId, 12))
                    CustomMapHandler.SetMapBgm("RazielPhase2_Re21341.ogg", true, "Raziel_Re21341");
            }

            if (_model.Phase < 5) return;
            _model.Owner.forceRetreat = true;
            if (BattleObjectManager.instance.GetAliveList(UnitUtil.ReturnOtherSideFaction(_model.Owner.faction)).Count >
                0)
                _model.Owner.Die();
        }

        public void RazielIsDeadBeforeTurn6()
        {
            if (_model.Phase > 6) return;
            if (!_model.Owner.IsDead()) return;
            UnitUtil.UnitReviveAndRecovery(_model.Owner, _model.Owner.MaxHp, false);
            UnitUtil.BattleAbDialog(_model.Owner.view.dialogUI,
                new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "RazielEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("RazielImmortal_Re21341")).Value.Desc
                    }
                }, AbColorType.Negative);
        }
    }
}