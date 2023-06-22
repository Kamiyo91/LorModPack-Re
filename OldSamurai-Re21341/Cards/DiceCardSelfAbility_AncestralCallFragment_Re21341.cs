using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.OldSamurai_Re21341.Cards
{
    public class DiceCardSelfAbility_AncestralCallFragment_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate();
            self.exhaust = true;
        }

        private static void Activate()
        {
            SummonSpecialUnit();
            UnitUtil.RefreshCombatUI();
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public static BattleUnitModel SummonSpecialUnit()
        {
            var unit = new UnitModelRoot
            {
                PackageId = KamiyoModParameters.PackageId,
                Id = 10000009,
                UnitNameId = 9,
                SummonedOnPlay = true,
                AutoPlay = true,
                LockedEmotion = true
            };
            return UnitUtil.AddNewUnitPlayerSideCustomData(unit,
                BattleObjectManager.instance.GetList(Faction.Player).Count);
        }
    }
}