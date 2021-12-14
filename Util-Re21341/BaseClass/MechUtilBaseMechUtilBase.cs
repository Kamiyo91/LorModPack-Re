﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using LOR_XML;
using Util_Re21341.CommonBuffs;

namespace Util_Re21341.BaseClass
{
    public class MechUtilBase
    {
        private readonly MechUtilBaseModel _model;

        public MechUtilBase(MechUtilBaseModel model)
        {
            _model = model;
            if (model.HasEgo && model.EgoCardId != null) model.Owner.personalEgoDetail.AddCard(model.EgoCardId);
        }

        public virtual void SurviveCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.Hp || !_model.Survive) return;
            _model.Survive = false;
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEnd_Re21341());
            _model.Owner.SetHp(_model.SetHp);
            UnitUtil.UnitReviveAndRecovery(_model.Owner,0,_model.RecoverLightOnSurvive);
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmunityToStatusAliment_Re21341());
            if (_model.HasSurviveAbDialog) UnitUtil.BattleAbDialog(_model.Owner.view.dialogUI, _model.SurviveAbDialogList, _model.SurviveAbDialogColor);
            if (_model.NearDeathBuffExist) _model.Owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(_model.NearDeathBuffType));
        }
        public virtual void EgoActive()
        {
            if (_model.Owner.bufListDetail.HasAssimilation()) return;
            _model.EgoActivated = false;
            if (_model.IsSummonEgo && BattleObjectManager.instance.GetAliveList(Faction.Player).Count > 1)
            {
                _model.Owner.personalEgoDetail.AddCard(_model.SecondaryEgoCardId);
                return;
            }
            if (!string.IsNullOrEmpty(_model.SkinName)) _model.Owner.view.SetAltSkin(_model.SkinName);
            _model.Owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(_model.EgoType));
            _model.Owner.breakDetail.ResetGauge();
            _model.Owner.breakDetail.RecoverBreakLife(1, true);
            _model.Owner.cardSlotDetail.RecoverPlayPoint(_model.Owner.cardSlotDetail.GetMaxPlayPoint());
            if (_model.HasEgoAttack) _model.Owner.personalEgoDetail.AddCard(_model.EgoAttackCardId);
            if (_model.RefreshUI) UnitUtil.RefreshCombatUI();
            if (_model.HasEgoAbDialog) UnitUtil.BattleAbDialog(_model.Owner.view.dialogUI, _model.EgoAbDialogList, _model.EgoAbColorColor);
        }
        public virtual void OnUseExpireCard(LorId cardId)
        {
            if (_model.LorIdArray != null && _model.LorIdArray.Contains(cardId))
                _model.Owner.personalEgoDetail.RemoveCard(cardId);
            if (_model.EgoAttackCardExpire && _model.EgoAttackCardId == cardId)
                _model.Owner.personalEgoDetail.RemoveCard(_model.EgoAttackCardId);
            if (!_model.HasEgo || _model.EgoCardId != cardId) return;
            if (_model.EgoCardId != null) _model.Owner.personalEgoDetail.RemoveCard(_model.EgoCardId);
            if (_model.HasAdditionalPassive) _model.Owner.passiveDetail.AddPassive(_model.AdditionalPassiveId);
            _model.EgoActivated = true;
        }

        public virtual void TurnEgoAbDialogOff() => _model.HasEgoAbDialog = false;
        public virtual bool EgoCheck() => _model.EgoActivated;
        public virtual void ForcedEgo() => _model.EgoActivated = true;
        public virtual void SetVipUnit() => _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Vip_Re21341());
        public virtual void ChangeEgoAbDialog(List<AbnormalityCardDialog> value) => _model.EgoAbDialogList = value;
    }
}
