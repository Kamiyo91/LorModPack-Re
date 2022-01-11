using System.Linq;
using Util_Re21341.CommonPassives;

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
            if (unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Solo_Re21341) is
                PassiveAbility_Solo_Re21341 passive) passive.SetCardValue(true);
            foreach (var allyUnit in BattleObjectManager.instance.GetAliveList(unit.faction).Where(x => x != unit))
                allyUnit.Die();
        }
    }
}