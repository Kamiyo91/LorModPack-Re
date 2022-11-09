using System.Linq;
using BigDLL4221.Utils;
using KamiyoModPack.Wilton_Re21341.Buffs;

namespace KamiyoModPack.Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_WiltonSpecialCard_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_Vengeance_Re21341)
                ?.stack > 29;
        }

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit, targetUnit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit, BattleUnitModel targetUnit)
        {
            if (!(unit.bufListDetail.GetActivatedBufList()
                        .FirstOrDefault(x => x is BattleUnitBuf_Vengeance_Re21341) is BattleUnitBuf_Vengeance_Re21341
                    buff)) return;
            targetUnit.TakeDamage(buff.stack);
            buff.OnAddBuf(-99);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, unit);
            unit.cardSlotDetail.RecoverPlayPoint(1);
            targetUnit.bufListDetail.RemoveBufAll(BufPositiveType.Positive);
            targetUnit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Weak, 1, unit);
            ArtUtil.BaseGameLoadPrefabEffect(targetUnit,
                "Battle/DiceAttackEffects/New/FX/DamageDebuff/FX_DamageDebuff_Blooding", "Buf/Effect_Bleeding");
        }
    }
}