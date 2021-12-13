using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Hayate_Re21341.Buffs;
using Util_Re21341.BaseClass;

namespace Hayate_Re21341.MechUtil
{
    public class MechUtil_Hayate : MechUtilBase
    {
        private readonly BattleUnitBuf_EntertainMe_Re21341 _buf;
        public MechUtil_Hayate(MechUtilBaseModel model) : base(model)
        {
            _buf = new BattleUnitBuf_EntertainMe_Re21341();
            model.Owner.bufListDetail.AddBufWithoutDuplication(_buf);
        }
        public override void OnUseExpireCard(LorId cardId)
        {
            if (cardId == new LorId(ModParameters.PackageId, 1))
                _buf.stack = 0;
            base.OnUseExpireCard(cardId);
        }
    }
}
