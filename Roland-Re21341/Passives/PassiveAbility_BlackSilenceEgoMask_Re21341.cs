using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Roland_Re21341.Buffs;
using Util_Re21341;

namespace Roland_Re21341.Passives
{
    public class PassiveAbility_BlackSilenceEgoMask_Re21341 : PassiveAbilityBase
    {
        private MechUtil_Roland _util;
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
            UnitUtil.ReturnToTheOriginalSkin(owner, "BlackSilence");
            _util = new MechUtil_Roland(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                SkinName = "BlackSilence3",
                EgoType = typeof(BattleUnitBuf_BlackSilenceEgoMask_Re21341),
                EgoCardId = new LorId(ModParameters.PackageId, 31),
                HasEgoAttack = true,
                RefreshUI = true,
                EgoAttackCardId = new LorId(ModParameters.PackageId, 30)
            });
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard) => _util.OnUseExpireCard(curCard.card.GetID());
        public override void OnRoundEndTheLast()
        {
            if (_util.EgoCheck()) _util.EgoActive();
        }
    }
}
