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
    }
}