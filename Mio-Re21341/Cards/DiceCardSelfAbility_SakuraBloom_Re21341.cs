using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Buffs;

namespace KamiyoModPack.Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_SakuraBloom_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 3;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            var speedDiceResultValue = card.speedDiceResultValue;
            var target = card.target;
            var targetSlotOrder = card.targetSlotOrder;
            if (targetSlotOrder < 0 || targetSlotOrder >= target.speedDiceResult.Count) return;
            var speedDice = target.speedDiceResult[targetSlotOrder];
            var targetDiceBroken = target.speedDiceResult[targetSlotOrder].breaked;
            if (speedDiceResultValue - speedDice.value <= Check && !targetDiceBroken) return;
            owner.TakeDamage(9, DamageType.Card_Ability, owner);
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                power = 1
            });
            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_SakuraPetal_Re21341) is
                BattleUnitBuf_SakuraPetal_Re21341) return;
            var buf = new BattleUnitBuf_SakuraPetal_Re21341();
            owner.bufListDetail.AddBufWithoutDuplication(buf);
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 59));
        }
    }
}