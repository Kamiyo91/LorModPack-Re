using System;
using BLL_Re21341.Models.MechUtilModels;
using Util_Re21341.CommonBuffs;

namespace Util_Re21341.BaseClass
{
    public class MechUtilBase
    {
        private MechUtilBaseModel model;

        public MechUtilBase(MechUtilBaseModel model)
        {
            this.model = model;
        }

        public virtual void SurviveCheck(int dmg)
        {
            if (!(model.Owner.hp - dmg <= model.Hp) || !model.Survive) return;
            model.Survive = false;
            model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEnd_Re21341());
            model.Owner.SetHp(model.SetHp);
            if(model.NearDeathBuffExist) model.Owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(model.NearDeathBuffType));
            if (model.HasEgo) ForcedEgo();
        }
        public virtual void EgoActive()
        {
            if (model.Owner.bufListDetail.HasAssimilation()) return;
            model.EgoActivated = false;
            model.Owner.personalEgoDetail.RemoveCard(model.EgoCardId);
            if(!string.IsNullOrEmpty(model.SkinName)) model.Owner.view.SetAltSkin(model.SkinName);
            model.Owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(model.EgoType));
            model.Owner.breakDetail.ResetGauge();
            model.Owner.breakDetail.RecoverBreakLife(1,true);
            model.Owner.cardSlotDetail.RecoverPlayPoint(model.Owner.cardSlotDetail.GetMaxPlayPoint());
            if(model.RefreshUI) UnitUtil.RefreshCombatUI();
        }
        public virtual void OnUseExpireCard(LorId cardId)
        {
            if (model.LorIdArray != null && model.LorIdArray.Contains(cardId))
            {
                model.Owner.personalEgoDetail.RemoveCard(cardId);
            }

            if (model.HasEgo && model.EgoCardId == cardId)
            {
                model.EgoActivated = true;
            }
        }
        public virtual bool EgoCheck() => model.EgoActivated;
        public virtual bool ForcedEgo() => model.EgoActivated = true;
    }
}
