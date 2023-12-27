using System.Collections.Generic;
using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_RazielFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override string SkinName => "Raziel_Re21341";

        public MapModelRoot GetMapModel()
        {
            return new MapModelRoot
            {
                Component = "Raziel_Re21341MapManager",
                Stage = "Raziel_Re21341",
                Bgy = 0.375f,
                Fy = 0.225f,
                OriginalMapStageIds = new List<LorIdRoot>
                {
                    new LorIdRoot { Id = 7, PackageId = KamiyoModParameters.PackageId },
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
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction))
            {
                unit.RecoverHP(10);
                unit.breakDetail.RecoverBreak(10);
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