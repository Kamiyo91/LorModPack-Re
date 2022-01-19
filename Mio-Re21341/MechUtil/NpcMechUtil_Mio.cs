using BLL_Re21341.Models.MechUtilModels;
using Sound;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Mio_Re21341.MechUtil
{
    public class NpcMechUtil_Mio : NpcMechUtilBase
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
    }
}