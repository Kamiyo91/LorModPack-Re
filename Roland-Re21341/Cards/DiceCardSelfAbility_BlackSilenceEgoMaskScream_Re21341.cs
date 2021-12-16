using BLL_Re21341.Models;
using Roland_Re21341.Buffs;
using Util_Re21341;

namespace Roland_Re21341.Cards
{
    public class DiceCardSelfAbility_BlackSilenceEgoMaskScream_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _used;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.HasBuf<BattleUnitBuf_BlackSilenceEgoMask_Re21341>();
        }

        public override void OnUseCard()
        {
            owner.view.SetAltSkin("BlackSilence4");
        }

        public override void OnStartBattle()
        {
            _used = true;
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
            owner.view.SetAltSkin("BlackSilence3");
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!_used) return;
            _used = false;
            MapUtil.ReturnFromEgoMap("BlackSilenceMassEgo_Re21341", 0);
        }
    }
}