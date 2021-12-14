using BLL_Re21341.Models;
using Util_Re21341;

namespace Roland_Re21341.Cards
{
    public class DiceCardSelfAbility_BlackSilenceEgoMaskScream_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.HasAssimilation();
        }

        public override void OnUseCard()
        {
            owner.view.SetAltSkin("BlackSilence4");
        }

        public override void OnStartBattle()
        {
            if (SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            ChangeToBlackSilenceEgoMap(owner);
        }

        private static void ChangeToBlackSilenceEgoMap(BattleUnitModel owner)
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "BlackSilenceMassEgo_Re21341",
                OneTurnEgo = true,
                IsPlayer = true,
                Component = new BlackSilence_Re21341MapManager(),
                InitBgm = false,
                Fy = 0.285f
            }, owner.faction);
        }

        public override void OnEndBattle()
        {
            if (string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin))
                owner.view.SetAltSkin("BlackSilence3");
            else
                owner.view.CreateSkin();
        }
    }
}
