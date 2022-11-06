using BigDLL4221.Buffs;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.OldSamurai_Re21341.Buffs;

namespace KamiyoModPack.OldSamurai_Re21341.Cards
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
            unit.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 3));
            unit.bufListDetail.AddBuf(new BattleUnitBuf_DeepBreathing_Re21341());
            if (unit.faction == Faction.Player && !unit.bufListDetail.HasBuf<BattleUnitBuf_Uncontrollable_DLL4221>())
                unit.passiveDetail.OnCreated();
        }
    }
}