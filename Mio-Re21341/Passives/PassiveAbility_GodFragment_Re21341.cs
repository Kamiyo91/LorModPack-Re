using BigDLL4221.Extensions;
using BigDLL4221.Passives;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Buffs;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragment_Re21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new MioUtil().MioPlayerUtil);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            owner.allyCardDetail.DrawCards(1);
            var speedDiceResultValue = curCard.speedDiceResultValue;
            var target = curCard.target;
            var targetSlotOrder = curCard.targetSlotOrder;
            if (targetSlotOrder < 0 || targetSlotOrder >= target.speedDiceResult.Count) return;
            var speedDice = target.speedDiceResult[targetSlotOrder];
            var targetDiceBroken = target.speedDiceResult[targetSlotOrder].breaked;
            if (speedDiceResultValue - speedDice.value <= 0 && !targetDiceBroken) return;
            owner.GetActiveBuff<BattleUnitBuf_GodAuraRelease_Re21341>()?.OnAddBuf(1);
        }
    }
}