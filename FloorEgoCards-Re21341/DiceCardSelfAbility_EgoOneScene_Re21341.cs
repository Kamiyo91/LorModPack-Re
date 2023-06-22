using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_EgoOneScene_Re21341 : DiceCardSelfAbilityBase
    {
        public bool MapActivated;
        public virtual MapModelRoot MapModel => null;
        public virtual string SkinName => "";
        public string PackageId => KamiyoModParameters.PackageId;

        public override void OnUseCard()
        {
            if (!string.IsNullOrEmpty(SkinName))
            {
                owner.view.StartEgoSkinChangeEffect("Character");
                owner.view.SetAltSkin(SkinName);
            }

            ChangeToEgoMap();
        }

        public override void OnEndAreaAttack()
        {
            if (string.IsNullOrEmpty(SkinName)) return;
            owner.view.StartEgoSkinChangeEffect("Character");
            owner.view.CreateSkin();
        }

        public void ChangeToEgoMap()
        {
            if (MapModel == null || SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            if (MapUtil.ChangeMap(CustomMapHandler.GetCMU(PackageId), MapModel)) MapActivated = true;
        }

        public virtual void ReturnFromEgoMap()
        {
            MapActivated = false;
            if (MapModel == null) return;
            MapUtil.ReturnFromEgoMap(CustomMapHandler.GetCMU(PackageId), MapModel.Stage,
                MapModel.OriginalMapStageIds.Select(x => x.ToLorId()).ToList());
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (MapActivated) ReturnFromEgoMap();
        }
    }
}