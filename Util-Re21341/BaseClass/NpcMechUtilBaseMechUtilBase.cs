using BLL_Re21341.Models.MechUtilModels;

namespace Util_Re21341.BaseClass
{
    public class NpcMechUtilBase : MechUtilBase
    {
        private readonly NpcMechUtilBaseModel _model;
        private bool _oneTurnCard;
        public NpcMechUtilBase(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }
        public virtual void OnUseCardResetCount(LorId cardId)
        {
            if (_model.LorIdArrayMass != null && _model.LorIdArrayMass == cardId)
            {
                _model.Counter = 0;
            }
        }

        public virtual void RaiseCounter()
        {
            if (_model.MassAttackCount && _model.Counter < _model.MaxCounter) _model.Counter++;
        }

        public virtual void SetOneTurnCard(bool value) => _oneTurnCard = value;
        public virtual void SetCounter(int value) => _model.Counter = value;
        public virtual void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin,LorId cardId)
        {
            if (_model.Owner.IsBreakLifeZero() || _model.Counter < _model.MaxCounter && _oneTurnCard)
                return;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(cardId));
            SetOneTurnCard(true);
        }
    }
}
