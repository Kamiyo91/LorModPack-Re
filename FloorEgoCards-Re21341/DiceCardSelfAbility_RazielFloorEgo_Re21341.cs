using BigDLL4221.CardAbility;
using BigDLL4221.Models;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_RazielFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_DLL4221
    {
        public override MapModel MapModel => KamiyoModParameters.RazielMap;
        public override string SkinName => "Raziel_Re21341";

        public override void OnUseCard()
        {
            base.OnUseCard();
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction))
            {
                unit.RecoverHP(10);
                unit.breakDetail.RecoverBreak(10);
            }
        }
    }
}