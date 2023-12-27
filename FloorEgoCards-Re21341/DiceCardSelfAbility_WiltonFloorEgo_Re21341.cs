using System.Collections.Generic;
using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_WiltonFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override string SkinName => "Wilton_Re21341";

        public MapModelRoot GetMapModel()
        {
            return new MapModelRoot
            {
                Component = "Wilton_Re21341MapManager",
                Stage = "Wilton_Re21341",
                Bgy = 0.2f,
                OriginalMapStageIds = new List<LorIdRoot>
                {
                    new LorIdRoot { Id = 6, PackageId = KamiyoModParameters.PackageId },
                    new LorIdRoot { Id = 11, PackageId = KamiyoModParameters.PackageId },
                    new LorIdRoot { Id = 12, PackageId = KamiyoModParameters.PackageId }
                }
            };
        }

        public override void OnUseCard()
        {
            var mapModel = GetMapModel();
            if (!string.IsNullOrEmpty(SkinName))
            {
                owner.view.StartEgoSkinChangeEffect("Character");
                owner.view.SetAltSkin(SkinName);
            }

            ChangeToEgoMap(mapModel);
            foreach (var unit in BattleObjectManager.instance.GetAliveList(
                         owner.faction.ReturnOtherSideFaction()))
            {
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Bleeding, 3, owner);
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 3, owner);
            }
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!MapActivated) return;
            var mapModel = GetMapModel();
            ReturnFromEgoMap(mapModel);
        }
    }
}