using System.Collections.Generic;
using System.Linq;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.OldSamurai_Re21341.Buffs;
using KamiyoModPack.OldSamurai_Re21341.MapManager;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.OldSamurai_Re21341.Passives
{
    public class PassiveAbility_OldSamurai_Re21341 : PassiveAbilityBase
    {
        private readonly LorId _egoCard = new LorId(KamiyoModParameters.PackageId, 8);
        public bool EgoActive;
        public bool EgoActiveQueue;
        public bool MapActive;

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "OldSamuraiPlayer_Re21341MapManager",
            Stage = "OldSamurai_Re21341",
            OneTurnEgo = false,
            Bgy = 0.2f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 1, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public override void OnWaveStart()
        {
            UnitUtil.CheckSkinProjection(owner);
            owner.personalEgoDetail.AddCard(_egoCard);
        }

        public override void OnRoundStart()
        {
            if (!EgoActiveQueue) return;
            EgoActiveQueue = false;
            owner.personalEgoDetail.RemoveCard(_egoCard);
            owner.EgoActiveWithMapChange<BattleUnitBuf_SamuraiEgo_Re21341, OldSamuraiPlayer_Re21341MapManager>(
                ref EgoActive, ref MapActive, refreshUI: true, mapModel: MapModel);
            var unitModel = new UnitModelRoot
            {
                PackageId = KamiyoModParameters.PackageId, Id = 10000002, UnitNameId = 2, LockedEmotion = true
            };
            for (var i = 0; i < 3; i++)
                UnitUtil.AddNewUnitPlayerSideCustomData(unitModel,
                    BattleObjectManager.instance.GetList(owner.faction).Count);
        }

        public override void OnBattleEnd()
        {
            owner.UnitReviveAndRecovery(owner.MaxHp, false);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 8:
                    EgoActiveQueue = true;
                    break;
            }
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x.passiveDetail.HasPassive<PassiveAbility_GhostSamurai_Re21341>()))
                unit.Die();
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!MapActive) return;
            if (!owner.IsDead() && BattleObjectManager.instance.GetAliveList(owner.faction)
                    .Any(x => x.passiveDetail.HasPassive<PassiveAbility_GhostSamurai_Re21341>())) return;
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_SamuraiEgo_Re21341));
            MapUtil.ReturnFromEgoAssimilationMap(KamiyoModParameters.PackageId, ref MapActive, _egoCard);
        }
    }
}