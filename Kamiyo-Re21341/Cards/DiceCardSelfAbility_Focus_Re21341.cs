using Sound;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_Focus_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            //ArtUtil.BaseGameLoadPrefabEffect(unit, "Battle/DiceAttackEffects/New/FX/DamageDebuff/FX_DamageDebuff_Fire",
            //    "Buf/Effect_Burn");
            SoundEffectPlayer.PlaySound("Creature/Helper_FullCharge");
            unit.TakeDamage(10);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, unit);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, unit);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }
    }
}