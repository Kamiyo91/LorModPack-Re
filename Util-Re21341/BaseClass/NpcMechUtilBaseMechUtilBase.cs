using BLL_Re21341.Models.MechUtilModels;

namespace Util_Re21341.BaseClass
{
    public class NpcMechUtilBase : MechUtilBase
    {
        private NpcMechUtilBaseModel model;
        private bool _oneTurnCard;
        public NpcMechUtilBase(NpcMechUtilBaseModel model) : base(model)
        {
            this.model = model;
        }
        public virtual void OnUseCardResetCount(LorId cardId)
        {
            if (model.LorIdArrayMass != null && model.LorIdArrayMass == cardId)
            {
                model.Counter = 0;
            }
        }

        public virtual void RaiseCounter()
        {
            if (model.MassAttackCount && model.Counter < model.MaxCounter) model.Counter++;
        }

        public virtual void SetOneTurnCard(bool value) => _oneTurnCard = value;
        public virtual void SetCounter(int value) => model.Counter = value;
        public virtual void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin,LorId cardId)
        {
            if (model.Owner.IsBreakLifeZero() || model.Counter < model.MaxCounter && _oneTurnCard)
                return;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(cardId));
            SetOneTurnCard(true);
        }
    }
}
