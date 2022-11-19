using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

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
            return UnitUtil.AddNewUnitPlayerSideCustomData(KamiyoModParameters.SamuraiGhostPlayerEmotion,
                BattleObjectManager.instance.GetList(Faction.Player).Count);
        }
    }
}