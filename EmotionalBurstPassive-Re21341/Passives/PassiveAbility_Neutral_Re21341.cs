using System.Linq;
using EmotionalBurstPassive_Re21341.Buffs;

namespace EmotionalBurstPassive_Re21341.Passives
{
    public class PassiveAbility_Neutral_Re21341 : PassiveAbilityBase
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            Hide();
        }

        public override void OnRoundStartAfter()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }

        public void RemoveBuff()
        {
            owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_Neutral_Re21341)
                ?.Destroy();
        }
    }
}