using KamiyoModPack.Kamiyo_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_ShockAbsorb_Re21341 : DiceCardSelfAbilityBase
    {
        public bool Active;

        public override string[] Keywords => new[] { "Endurance", "Strength" };
        //public override bool OnChooseCard(BattleUnitModel owner)
        //{
        //    return owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>();
        //}

        public override void OnStartBattle()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>()) return;
            var buff = owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>();
            if (buff == null) return;
            var positiveNum = buff.stack;
            if (positiveNum > 0)
                positiveNum /= 10;
            if (positiveNum == 0) return;
            Active = buff.stack > 24;
            owner.TakeDamage(positiveNum * 5);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, positiveNum, owner);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, positiveNum, owner);
        }
    }
}