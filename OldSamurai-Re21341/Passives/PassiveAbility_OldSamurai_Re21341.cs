using BLL_Re21341.Models;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.BaseClass;
using KamiyoStaticUtil.Utils;
using OldSamurai_Re21341.Buffs;

namespace OldSamurai_Re21341.Passives
{
    public class PassiveAbility_OldSamurai_Re21341 : PassiveAbilityBase
    {
        private MechUtilBase _util;

        public override void OnWaveStart()
        {
            _util = new MechUtilBase(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                IsSummonEgo = true,
                EgoType = typeof(BattleUnitBuf_OldSamuraiEgo_Re21341),
                EgoCardId = new LorId(KamiyoModParameters.PackageId, 8),
                SecondaryEgoCardId = new LorId(KamiyoModParameters.PackageId, 901)
            });
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }

        public override void OnDie()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_OldSamuraiEgo_Re21341>()) return;
            UnitUtil.VipDeathNpc(owner);
        }

        public void ForcedEgo()
        {
            _util.ForcedEgo();
        }
    }
}