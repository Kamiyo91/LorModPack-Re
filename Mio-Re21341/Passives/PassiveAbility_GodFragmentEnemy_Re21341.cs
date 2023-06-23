using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Buffs;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragmentEnemy_Re21341 : PassiveAbilityBase
    {
        private const string OriginalSkinName = "MioNormalEye_Re21341";
        private const string EgoSkinName = "MioRedEye_Re21341";

        private readonly bool _additionalUnit =
            Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 2;

        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly bool _mapActive = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 9;

        public LorId AttackCard = new LorId(KamiyoModParameters.PackageId, 900);
        public int Counter;
        public bool CreatureFilter;
        public bool EgoActive;

        public List<AbnormalityCardDialog> EgoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "MioEnemy",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("MioEnemyEgoActive1_Re21341")).Value?.Desc ?? ""
            }
        };

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "Mio_Re21341MapManager",
            Stage = "Mio_Re21341",
            Bgy = 0.2f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 2, PackageId = KamiyoModParameters.PackageId },
                new LorIdRoot { Id = 9, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public string MapName = "Mio_Re21341";

        public int MaxCounter = 4;
        public bool MechChanging;
        public string MusicFileName = "MioPhase2_Re21341.ogg";
        public bool OneTurnCard;
        public int Phase;
        public int PhaseHp = 271;
        public string SaveDataId = "MioSave21341";
        public int SpeedCount;
        public bool Survived;

        public List<AbnormalityCardDialog> SurviveDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "MioEnemy",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("MioEnemySurvive1_Re21341")).Value?.Desc ?? ""
            }
        };

        public override void OnWaveStart()
        {
            if (_mapActive || _additionalUnit)
            {
                MapUtil.InitEnemyMap<Mio_Re21341MapManager>(_cmh,
                    MapModel);
                _cmh.EnforceMap();
            }

            Phase = NpcMechUtil.RestartPhase(SaveDataId);
            if (Phase != 0) ChangePhase(Phase, true);
        }

        public override void OnRoundStartAfter()
        {
            if (CreatureFilter) MapUtil.ActiveCreatureBattleCamFilterComponent();
        }

        public override void OnRoundStart()
        {
            if (_mapActive || _additionalUnit) _cmh.EnforceMap();
            owner.RemoveImmortalBuff();
            OneTurnCard = false;
            MechChanging = false;
            if (Phase == 0) return;
            Counter++;
            Mathf.Clamp(Counter, 0, MaxCounter);
        }

        public override void OnRoundEndTheLast()
        {
            if (SpeedCount > 2) owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, SpeedCount / 3, owner);
            if (!MechChanging) return;
            Phase++;
            ChangePhase(Phase);
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (OneTurnCard || Counter < MaxCounter) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            OneTurnCard = true;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(AttackCard));
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override int SpeedDiceNumAdder()
        {
            return Phase == 0 ? 2 : 3;
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (Phase < 1 && !MechChanging) return owner.MechHpCheck(dmg, PhaseHp, ref MechChanging);
            if (Phase >= 2) return base.BeforeTakeDamage(attacker, dmg);
            if (owner.SurviveCheck<BattleUnitBuf>(dmg, 0, ref Survived, 62, recoverLight: true, dialog: SurviveDialog,
                    color: Color.red)) MechChanging = true;
            return base.BeforeTakeDamage(attacker, dmg);
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
                    owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
                    Counter = 0;
                    MapUtil.ChangeMapGeneric<Mio_Re21341MapManager>(_cmh, MapModel);
                    break;
            }
        }

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { OriginalSkinName };
            owner.OnEndBattleSave(SaveDataId, Phase);
        }

        public override int ChangeTargetSlot(BattleDiceCardModel card, BattleUnitModel target, int currentSlot,
            int targetSlot, bool teamkill)
        {
            return UnitUtil.AlwaysAimToTheSlowestDice(target, targetSlot, true);
        }

        private void ChangePhase(int phase, bool restart = false)
        {
            switch (phase)
            {
                case 1:
                    owner.LevelUpEmotion(4 - owner.emotionDetail.EmotionLevel);
                    owner.ChangeCardCostByValue(-1, 99, true);
                    owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
                    Counter = 4;
                    var unitModel = new UnitModelRoot
                    {
                        PackageId = KamiyoModParameters.PackageId, Id = 10000900, UnitNameId = 3,
                        AdditionalPassiveIds = new List<LorIdRoot>
                        {
                            new LorIdRoot { Id = 37, PackageId = KamiyoModParameters.PackageId },
                            new LorIdRoot { Id = 57, PackageId = KamiyoModParameters.PackageId }
                        }
                    };
                    var unit = UnitUtil.AddNewUnitWithDefaultData(unitModel,
                        BattleObjectManager.instance.GetList(owner.faction.ReturnOtherSideFaction()).Count, true, 4);
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_CorruptedGodAuraRelease_Re21341>(ref EgoActive, EgoSkinName, true,
                            false, null, EgoDialog, Color.red);
                    var passive = unit.GetActivePassive<PassiveAbility_GodFragment_Re21341>();
                    if (passive != null)
                    {
                        passive.ForcedEgo();
                        passive.SpeedCount = 15;
                    }

                    if (!_mapActive && !_additionalUnit) break;
                    CreatureFilter = true;
                    _cmh.SetMapBgm(MusicFileName, true, MapName);
                    break;
                case 2:
                    owner.ChangeCardCostByValue(restart ? -2 : -1, 99, true);
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_CorruptedGodAuraRelease_Re21341>(ref EgoActive, EgoSkinName, true,
                            false, null, EgoDialog, Color.red);
                    Counter = 4;
                    if (_mapActive || _additionalUnit)
                    {
                        CreatureFilter = true;
                        if (restart) _cmh.SetMapBgm(MusicFileName, true, MapName);
                    }

                    break;
            }
        }

        public override void OnDie()
        {
            MapUtil.ActiveCreatureBattleCamFilterComponent(false);
        }
    }
}