using BigDLL4221.Extensions;
using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_ShockAbsorb_Re21341 : DiceCardSelfAbilityBase
    {
        public bool Active;

        //public override bool OnChooseCard(BattleUnitModel owner)
        //{
        //    return owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>();
        //}

        public override void OnStartBattle()
        {
            var buff = owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>();
            if (buff == null)
            {
                buff = new BattleUnitBuf_Shock_Re21341();
                owner.bufListDetail.AddBuf(buff);
            }

            buff.OnAddBuf(3);
            var positiveNum = buff.stack;
            if (positiveNum > 0)
                positiveNum /= 3;
            if (positiveNum == 0) return;
            Active = buff.stack > 9;
            buff.OnAddBuf(-99);
            owner.TakeDamage(positiveNum * 5);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, positiveNum, owner);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, positiveNum, owner);
        }
    }
}