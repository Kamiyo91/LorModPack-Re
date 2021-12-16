using BLL_Re21341.Models;
using OldSamurai_Re21341.Buffs;
using Util_Re21341.CommonBuffs;

namespace OldSamurai_Re21341.Cards
{
    public class DiceCardSelfAbility_DeepBreathing_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        public static void Activate(BattleUnitModel unit)
        {
            unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 3));
            unit.bufListDetail.AddBuf(new BattleUnitBuf_DeepBreathing_Re21341());
            if (unit.faction == Faction.Player && !unit.bufListDetail.HasBuf<BattleUnitBuf_Uncontrollable_Re21341>())
                unit.passiveDetail.OnCreated();
        }
    }
}