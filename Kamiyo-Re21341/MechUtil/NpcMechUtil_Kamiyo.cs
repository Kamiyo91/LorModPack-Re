using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Kamiyo_Re21341.MechUtil
{
    public class NpcMechUtil_Kamiyo : NpcMechUtilBase
    {
        public NpcMechUtil_Kamiyo(NpcMechUtilBaseModel model) : base(model)
        {
        }

        public override void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin)
        {
            if (Model.OneTurnCard && origin.GetID() == new LorId(KamiyoModParameters.PackageId, 22))
                origin = BattleDiceCardModel.CreatePlayingCard(
                    ItemXmlDataList.instance.GetCardItem(new LorId(KamiyoModParameters.PackageId,
                        RandomUtil.Range(20, 21))));
            base.OnSelectCardPutMassAttack(ref origin);
        }

        public override void ExtraMethodOnPhaseChangeRoundEnd(MechPhaseOptions mechOptions)
        {
            var card = Model.Owner.allyCardDetail.GetAllDeck()
                .FirstOrDefault(x => x.GetID() == new LorId(KamiyoModParameters.PackageId, 21));
            Model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
            Model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(KamiyoModParameters.PackageId, 22));
        }
    }
}