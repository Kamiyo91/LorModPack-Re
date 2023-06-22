using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_SakuraBloom_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 3;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (!card.CheckTargetSpeedByCard(Check)) return;
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