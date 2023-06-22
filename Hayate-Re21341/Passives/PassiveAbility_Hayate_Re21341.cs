using System.Collections.Generic;
using System.Linq;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Hayate_Re21341.Buffs;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_Hayate_Re21341 : PassiveAbilityBase
    {
        private readonly LorId _egoAttackCard = new LorId(KamiyoModParameters.PackageId, 29);
        private readonly LorId _egoCard = new LorId(KamiyoModParameters.PackageId, 28);

        private readonly List<AbnormalityCardDialog> _egoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "Hayate",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("HayateEgoActive1_Re21341")).Value?.Desc ?? ""
            },
            new AbnormalityCardDialog
            {
                id = "Hayate",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("HayateEgoActive2_Re21341")).Value?.Desc ?? ""
            }
        };

        private BattleUnitModel _fingersnapSpecialTarget;
        public bool BuffActive;
        public bool EgoActive;
        public bool EgoActiveQueue;

        public override void OnWaveStart()
        {
            UnitUtil.CheckSkinProjection(owner);
            BuffActive = true;
            owner.personalEgoDetail.AddCard(_egoCard);
        }

        public void SetActiveBuffValue(bool value)
        {
            BuffActive = value;
        }

        public override void OnRoundStart()
        {
            owner.CheckPermanentBuff<BattleUnitBuf_EntertainMe_Re21341>(BuffActive);
            if (!EgoActiveQueue) return;
            EgoActiveQueue = false;
            EgoActived();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 28:
                    EgoActiveQueue = true;
                    break;
                case 29:
                    owner.personalEgoDetail.RemoveCard(cardId.id);
                    break;
                case 907:
                    owner.personalEgoDetail.RemoveCard(cardId.id);
                    _fingersnapSpecialTarget = curCard.target;
                    break;
            }
        }

        public override void OnRoundEndTheLast()
        {
            if (_fingersnapSpecialTarget == null) return;
            BattleObjectManager.instance.UnregisterUnit(_fingersnapSpecialTarget);
            _fingersnapSpecialTarget = null;
            UnitUtil.RefreshCombatUI();
        }

        public void EgoActived()
        {
            owner.personalEgoDetail.RemoveCard(_egoCard);
            owner.EgoActive<BattleUnitBuf_TrueGodAuraRelease_Re21341>(ref EgoActive,
                emotionCardsId: new List<LorId> { _egoAttackCard }, dialog: _egoDialog, color: Color.green);
        }
    }
}