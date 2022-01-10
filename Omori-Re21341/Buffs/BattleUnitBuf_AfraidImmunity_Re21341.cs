using EmotionalBurstPassive_Re21341.Buffs;

namespace Omori_Re21341.Buffs
{
    public class BattleUnitBuf_AfraidImmunity_Re21341 : BattleUnitBuf
    {
        public override bool IsImmune(BattleUnitBuf buf)
        {
            return buf is BattleUnitBuf_Afraid_Re21341 || base.IsImmune(buf);
        }
    }
}