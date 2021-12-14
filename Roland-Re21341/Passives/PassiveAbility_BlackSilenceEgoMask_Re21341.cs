using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Roland_Re21341.Buffs;
using Util_Re21341;

namespace Roland_Re21341.Passives
{
    public class PassiveAbility_BlackSilenceEgoMask_Re21341 : PassiveAbilityBase
    {
        private MechUtil_Roland _util;
        public override void Init(BattleUnitModel self)
        {
            UnitUtil.ReturnToTheOriginalSkin(self, "BlackSilence");
            base.Init(self);
        }

        public override void OnBattleEnd()
        {
            UnitUtil.ReturnToTheOriginalSkin(owner, "BlackSilence");
        }
        public override void OnWaveStart()
        {
            if (!owner.passiveDetail.HasPassive<PassiveAbility_10012>())
            {
                owner.passiveDetail.DestroyPassive(this);
                return;
            }
            _util = new MechUtil_Roland(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                SkinName = "BlackSilence3",
                EgoType = typeof(BattleUnitBuf_BlackSilenceEgoMask_Re21341),
                EgoCardId = new LorId(ModParameters.PackageId, 28),
                HasEgoAttack = true,
                RefreshUI = true,
                EgoAttackCardId = new LorId(ModParameters.PackageId, 29)
            });
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard) => _util.OnUseExpireCard(curCard.card.GetID());
        public override void OnRoundEndTheLast()
        {
            if (_util.EgoCheck()) _util.EgoActive();
        }
    }
}
