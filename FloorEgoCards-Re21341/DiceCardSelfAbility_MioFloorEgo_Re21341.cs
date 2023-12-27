using System.Collections.Generic;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Buffs;
using UtilLoader21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_MioFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override string SkinName => "MioRedEye_Re21341";

        public MapModelRoot GetMapModel()
        {
            return new MapModelRoot
            {
                Component = "Mio_Re21341MapManager",
                Stage = "Mio_Re21341",
                Bgy = 0.2f,
                OriginalMapStageIds = new List<LorIdRoot>
                {
                    new LorIdRoot { Id = 2, PackageId = KamiyoModParameters.PackageId },
                    new LorIdRoot { Id = 9, PackageId = KamiyoModParameters.PackageId },
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
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 3, owner);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_SakuraPetalOneScene_Re21341());
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!MapActivated) return;
            var mapModel = GetMapModel();
            ReturnFromEgoMap(mapModel);
        }
    }
}