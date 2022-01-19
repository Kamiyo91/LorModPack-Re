using BLL_Re21341.Models.MechUtilModels;
using Util_Re21341;
using Util_Re21341.BaseClass;

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
    }
}