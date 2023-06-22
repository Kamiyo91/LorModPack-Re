using System.Collections.Generic;
using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_KamiyoFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override MapModelRoot MapModel => new MapModelRoot
        {
            Component = "Kamiyo2_Re21341MapManager",
            Stage = "Kamiyo2_Re21341",
            Bgy = 0.475f,
            Fy = 0.225f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 3, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public override string SkinName => "KamiyoMask_Re21341";
    }
}