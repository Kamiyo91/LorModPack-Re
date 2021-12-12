using System;
using BLL_Re21341.Models.MechUtilModels;
using Util_Re21341.CommonBuffs;

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
            if (_model.LorIdEgoMassAttack != null && _model.LorIdEgoMassAttack == cardId)
            {
                _model.Counter = 0;
            }
        }

        public virtual void MechHpCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.MechHp || !_model.HasMechOnHp) return;
            _model.HasMechOnHp = false;
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEnd_Re21341());
            _model.Owner.SetHp(_model.MechHp);
        }
        public override void SurviveCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.Hp || !_model.Survive) return;
            _model.Survive = false;
            if(_model.ReloadMassAttackOnLethal) SetCounter(_model.MaxCounter);
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEnd_Re21341());
            _model.Owner.SetHp(_model.SetHp);
            if (_model.HasSurviveAbDialog) UnitUtil.BattleAbDialog(_model.Owner.view.dialogUI, _model.SurviveAbDialogList, _model.SurviveAbDialogColor);
            if (_model.NearDeathBuffExist) _model.Owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(_model.NearDeathBuffType));
        }
        public virtual void RaiseCounter()
        {
            if (_model.MassAttackStartCount && _model.Counter < _model.MaxCounter) _model.Counter++;
        }
        public virtual void SetMassAttack(bool value) => _model.MassAttackStartCount = value;
        public virtual void SetOneTurnCard(bool value) => _oneTurnCard = value;
        public virtual void SetCounter(int value) => _model.Counter = value;
        public virtual void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin)
        {
            if (_model.Owner.IsBreakLifeZero() || !_model.MassAttackStartCount || _model.Counter < _model.MaxCounter || _oneTurnCard)
                return;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(_model.LorIdEgoMassAttack));
            SetOneTurnCard(true);
        }
    }
}
