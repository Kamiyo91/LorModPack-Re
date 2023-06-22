using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Raziel_Re21341.Buffs;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Raziel_Re21341.Passives
{
    public class PassiveAbility_Inquisitor_Re21341 : PassiveAbilityBase
    {
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly LorId _egoAttackCard = new LorId(KamiyoModParameters.PackageId, 58);
        private readonly LorId _egoCard = new LorId(KamiyoModParameters.PackageId, 57);

        private readonly List<AbnormalityCardDialog> _reviveDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "RazielEnemy",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("RazielImmortal_Re21341")).Value?.Desc ?? ""
            }
        };

        public bool EgoActive;
        public bool EgoActiveQueue;

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "Raziel_Re21341MapManager",
            Stage = "Raziel_Re21341",
            Bgy = 0.375f,
            Fy = 0.225f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 7, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public bool Revived;

        public override void OnWaveStart()
        {
            UnitUtil.CheckSkinProjection(owner);
            owner.personalEgoDetail.AddCard(_egoCard);
            owner.personalEgoDetail.AddCard(_egoAttackCard);
        }

        public override void OnRoundStart()
        {
            if (!EgoActiveQueue) return;
            EgoActiveQueue = false;
            owner.EgoActive<BattleUnitBuf_OwlSpirit_Re21341>(ref EgoActive);
            owner.personalEgoDetail.RemoveCard(_egoCard);
        }

        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = 1,
                dmgRate = 25
            });
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (Revived || !owner.IsDead()) return;
            owner.ReviveCheck(ref Revived, owner.MaxHp, true, _reviveDialog, Color.yellow, true);
            owner.ChangeCardCostByValue(-1, 3, false);
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
                case 57:
                    EgoActiveQueue = true;
                    break;
                case 58:
                    MapUtil.ChangeMapGeneric<Raziel_Re21341MapManager>(_cmh, MapModel);
                    break;
            }
        }
    }
}