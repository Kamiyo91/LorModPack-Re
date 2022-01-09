using EmotionalBurstPassive_Re21341.Buffs;

namespace Omori_Re21341
{
    public class DiceCardAbility_AfraidDice_Re21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            target?.bufListDetail.AddReadyBuf(new BattleUnitBuf_Afraid_Re21341());
        }
    }
}