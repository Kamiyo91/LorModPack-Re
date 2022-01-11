using System.Linq;

namespace Util_Re21341.CommonCards
{
    public class DiceCardSelfAbility_Solo_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            foreach (var allyUnit in BattleObjectManager.instance.GetAliveList(unit.faction).Where(x => x != unit))
                allyUnit.Die();
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public override bool IsTargetableAllUnit()
        {
            return true;
        }
    }
}