using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Wilton_Re21341.Buffs;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Wilton_Re21341.Passives
{
    public class PassiveAbility_KurosawaButler_Re21341 : PassiveAbilityBase
    {
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);

        private readonly List<LorId> _egoActivatedCards = new List<LorId>
            { new LorId(KamiyoModParameters.PackageId, 48), new LorId(KamiyoModParameters.PackageId, 30) };

        private readonly List<AbnormalityCardDialog> _egoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "Wilton",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("WiltonEgoActive1_Re21341")).Value?.Desc ?? ""
            }
        };

        public bool EgoActive;
        public bool EgoActiveQueue;
        public LorId EgoCard = new LorId(KamiyoModParameters.PackageId, 47);

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "Wilton_Re21341MapManager",
            Stage = "Wilton_Re21341",
            Bgy = 0.2f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 6, PackageId = KamiyoModParameters.PackageId },
                new LorIdRoot { Id = 11, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public override void OnWaveStart()
        {
            UnitUtil.CheckSkinProjection(owner);
            owner.personalEgoDetail.AddCard(EgoCard);
        }

        public override void OnRoundStart()
        {
            if (!EgoActiveQueue) return;
            EgoActiveQueue = false;
            owner.EgoActive<BattleUnitBuf_Vengeance_Re21341>(ref EgoActive, emotionCardsId: _egoActivatedCards,
                dialog: _egoDialog, color: Color.red);
            owner.personalEgoDetail.RemoveCard(EgoCard);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 30:
                    owner.personalEgoDetail.RemoveCard(cardId);
                    break;
                case 47:
                    EgoActiveQueue = true;
                    break;
                case 48:
                    MapUtil.ChangeMapGeneric<Wilton_Re21341MapManager>(_cmh, MapModel);
                    break;
            }
        }
    }
}