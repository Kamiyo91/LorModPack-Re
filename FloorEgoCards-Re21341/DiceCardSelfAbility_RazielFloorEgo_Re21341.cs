using System.Collections.Generic;
using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_RazielFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override MapModelRoot MapModel => new MapModelRoot
        {
            Component = "Raziel_Re21341MapManager",
            Stage = "Raziel_Re21341",
            Bgy = 0.375f,
            Fy = 0.225f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 7, PackageId = KamiyoModParameters.PackageId }
            }
        };

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