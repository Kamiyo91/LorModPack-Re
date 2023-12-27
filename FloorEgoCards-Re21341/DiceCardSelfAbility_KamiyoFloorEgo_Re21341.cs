using System.Collections.Generic;
using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_KamiyoFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override string SkinName => "KamiyoMask_Re21341";

        public MapModelRoot GetMapModel()
        {
            return new MapModelRoot
            {
                Component = "Kamiyo2_Re21341MapManager",
                Stage = "Kamiyo2_Re21341",
                Bgy = 0.475f,
                Fy = 0.225f,
                OriginalMapStageIds = new List<LorIdRoot>
                {
                    new LorIdRoot { Id = 3, PackageId = KamiyoModParameters.PackageId },
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
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!MapActivated) return;
            var mapModel = GetMapModel();
            ReturnFromEgoMap(mapModel);
        }
    }
}