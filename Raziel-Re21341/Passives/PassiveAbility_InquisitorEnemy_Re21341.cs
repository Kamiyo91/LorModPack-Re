using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Raziel_Re21341.Buffs;
using KamiyoModPack.Wilton_Re21341;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Raziel_Re21341.Passives
{
    public class PassiveAbility_InquisitorEnemy_Re21341 : PassiveAbilityBase
    {
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);

        private readonly List<AbnormalityCardDialog> _egoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "RazielEnemy",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("RazielEnemyEgoActive1_Re21341")).Value?.Desc ?? ""
            }
        };

        private readonly bool _mapActive = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 7;

        private readonly List<AbnormalityCardDialog> _reviveDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "RazielEnemy",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("RazielImmortal_Re21341")).Value?.Desc ?? ""
            }
        };

        public LorId AttackCard = new LorId(KamiyoModParameters.PackageId, 906);
        public int Counter;
        public bool EgoActive;

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

        public string MapName = "Raziel_Re21341";
        public int MaxCounter = 5;
        public bool MechChanging;
        public string MusicFileName = "RazielPhase2_Re21341.ogg";
        public bool OneTurnCard;
        public int Phase;
        public string SaveDataId = "RazielSave21341";
        public bool Survived;

        public override int SpeedDiceNumAdder()
        {
            return Phase == 0 ? 2 : 3;
        }

        public override void OnWaveStart()
        {
            if (_mapActive)
            {
                MapUtil.InitEnemyMap<Raziel_Re21341MapManager>(_cmh, MapModel);
                _cmh.EnforceMap();
            }

            Phase = NpcMechUtil.RestartPhase(SaveDataId);
            if (Phase != 0) ChangePhase(Phase);
        }

        public override void OnRoundStart()
        {
            if (_mapActive) _cmh.EnforceMap();
            owner.RemoveImmortalBuff();
            OneTurnCard = false;
            MechChanging = false;
            Counter++;
        }

        public override void OnRoundEndTheLast()
        {
            if (Counter >= MaxCounter) owner.DieFake();
            if (Phase < 1 && Counter > 1) MechChanging = true;
            if (!MechChanging) return;
            Phase++;
            ChangePhase(Phase);
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (OneTurnCard) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            OneTurnCard = true;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(AttackCard));
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead() || Counter >= MaxCounter) return;
            owner.ReviveCheck(ref Survived, owner.MaxHp, true, _reviveDialog, Color.yellow, true);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 906:
                    owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
                    if (MapModel == null) break;
                    MapUtil.ChangeMapGeneric<Wilton_Re21341MapManager>(_cmh, MapModel);
                    break;
            }
        }

        public override void OnBattleEnd()
        {
            owner.OnEndBattleSave(SaveDataId, Phase);
        }

        private void ChangePhase(int phase)
        {
            switch (phase)
            {
                case 1:
                    if (!owner.passiveDetail.HasPassive<PassiveAbility_InquisitorShimmering_Re21341>())
                        owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 41));
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_OwlSpirit_Re21341>(ref EgoActive, dialog: _egoDialog,
                            color: Color.red);
                    if (!_mapActive) break;
                    _cmh.SetMapBgm(MusicFileName, true, MapName);
                    break;
            }
        }
    }
}