using System.Collections.Generic;
using System.Linq;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Passives;
using KamiyoModPack.Mio_Re21341.Buffs;
using UnityEngine;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_MioMemory_Re21341 : PassiveAbilityBase
    {
        private const string OriginalSkinName = "MioNormalEye_Re21341";
        private const string EgoSkinName = "MioRedEye_Re21341";
        public LorId AttackCard = new LorId(KamiyoModParameters.PackageId, 900);
        public int Counter;
        public bool EgoActive;
        public int MaxCounter = 4;
        public bool OneTurnCard;
        public int SpeedCount;

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override void OnWaveStart()
        {
            Counter = 3;
            owner.EgoActive<BattleUnitBuf_GodAuraRelease_Re21341>(ref EgoActive, EgoSkinName, true);
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead()) return;
            var mainEnemy = BattleObjectManager.instance.GetAliveList(owner.faction)
                .FirstOrDefault(x => x.passiveDetail.HasPassive<PassiveAbility_AlterEgoNpc_Re21341>());
            if (mainEnemy == null || mainEnemy.hp < 162) return;
            owner.UnitReviveAndRecovery(owner.MaxHp, true);
        }

        public override void OnRoundStart()
        {
            OneTurnCard = false;
            Counter++;
            Mathf.Clamp(Counter, 0, MaxCounter);
        }

        public override void OnRoundEndTheLast()
        {
            if (SpeedCount > 2) owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, SpeedCount / 5, owner);
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (OneTurnCard || Counter < MaxCounter) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            OneTurnCard = true;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(AttackCard));
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.CheckTargetSpeedByCard(1))
            {
                SpeedCount++;
                Mathf.Clamp(SpeedCount, 0, 15);
            }

            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 900:
                    Counter = 0;
                    break;
            }
        }

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { OriginalSkinName };
        }
    }
}