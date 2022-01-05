using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Util_Re21341.BaseClass;

namespace Kamiyo_Re21341.MechUtil
{
    public class NpcMechUtil_Kamiyo : NpcMechUtilBase
    {
        private readonly NpcMechUtilBaseModel _model;

        public NpcMechUtil_Kamiyo(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public override void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin)
        {
            if (_model.OneTurnCard && origin.GetID() == new LorId(ModParameters.PackageId, 22))
                origin = BattleDiceCardModel.CreatePlayingCard(
                    ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, RandomUtil.Range(20, 21))));
            base.OnSelectCardPutMassAttack(ref origin);
        }
    }
}