using System;
using System.Threading.Tasks;
using BLL_Re21341.Models.MechUtilModels;
using Util_Re21341.CommonBuffs;

namespace Util_Re21341.BaseClass
{
    public class MechUtilBase
    {
        private readonly MechUtilBaseModel _model;

        public MechUtilBase(MechUtilBaseModel model)
        {
            _model = model;
        }

        public virtual void SurviveCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.Hp || !_model.Survive) return;
            _model.Survive = false;
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEnd_Re21341());
            _model.Owner.SetHp(_model.SetHp);
            if(_model.NearDeathBuffExist) _model.Owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(_model.NearDeathBuffType));
        }
        public virtual void EgoActive()
        {
            if (_model.Owner.bufListDetail.HasAssimilation()) return;
            _model.EgoActivated = false;
            if(_model.EgoCardId != null) _model.Owner.personalEgoDetail.RemoveCard(_model.EgoCardId);
            if(!string.IsNullOrEmpty(_model.SkinName)) _model.Owner.view.SetAltSkin(_model.SkinName);
            _model.Owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(_model.EgoType));
            _model.Owner.breakDetail.ResetGauge();
            _model.Owner.breakDetail.RecoverBreakLife(1,true);
            _model.Owner.cardSlotDetail.RecoverPlayPoint(_model.Owner.cardSlotDetail.GetMaxPlayPoint());
            if(_model.RefreshUI) UnitUtil.RefreshCombatUI();
        }
        public virtual void OnUseExpireCard(LorId cardId)
        {
            if (_model.LorIdArray != null && _model.LorIdArray.Contains(cardId))
            {
                _model.Owner.personalEgoDetail.RemoveCard(cardId);
            }

            if (!_model.HasEgo || _model.EgoCardId != cardId) return;
            _model.EgoActivated = true;
        }
        public virtual bool EgoCheck() => _model.EgoActivated;
        public virtual void ForcedEgo() => _model.EgoActivated = true;
    }
}
