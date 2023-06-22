using System.Collections.Generic;
using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_WiltonFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override MapModelRoot MapModel => new MapModelRoot
        {
            Component = "Wilton_Re21341MapManager",
            Stage = "Wilton_Re21341",
            Bgy = 0.2f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 6, PackageId = KamiyoModParameters.PackageId },
                new LorIdRoot { Id = 11, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public override string SkinName => "Wilton_Re21341";

        public override void OnUseCard()
        {
            base.OnUseCard();
            foreach (var unit in BattleObjectManager.instance.GetAliveList(
                         owner.faction.ReturnOtherSideFaction()))
            {
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Bleeding, 3, owner);
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 3, owner);
            }
        }
    }
}