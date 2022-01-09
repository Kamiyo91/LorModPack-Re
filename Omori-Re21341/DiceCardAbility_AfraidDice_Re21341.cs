using EmotionalBurstPassive_Re21341.Buffs;

namespace Omori_Re21341
{
    public class DiceCardAbility_AfraidDice_Re21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Afraid_Re21341) is
                BattleUnitBuf_Afraid_Re21341 buf)
            {
                buf.stack++;
            }
            else
            {
                buf = new BattleUnitBuf_Afraid_Re21341 { stack = 1 };
                target.bufListDetail.AddReadyBuf(buf);
            }
        }
    }
}