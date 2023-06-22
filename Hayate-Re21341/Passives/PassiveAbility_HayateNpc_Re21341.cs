using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Hayate_Re21341.Buffs;
using KamiyoModPack.Kamiyo_Re21341.Passives;
using KamiyoModPack.Mio_Re21341.Buffs;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_HayateNpc_Re21341 : PassiveAbilityBase
    {
        private readonly bool _additionalUnit =
            Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 4;

        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly bool _mapActive = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 10;

        private BattleUnitBuf_EntertainMe_Re21341 _buff;
        private List<BattleEmotionCardModel> _emotionCards = new List<BattleEmotionCardModel>();
        private BattleUnitModel _fingersnapSpecialTarget;

        public LorId AttackCard = new LorId(KamiyoModParameters.PackageId, 903);
        public bool CreatureFilter;
        public bool EgoActive;

        public List<AbnormalityCardDialog> EgoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "HayateEnemy",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("HayateEnemyEgoActive1_Re21341")).Value?.Desc ?? ""
            }
        };

        public MapModelRoot MapModel = new MapModelRoot
        {
            Component = "Hayate_Re21341MapManager",
            Stage = "Hayate_Re21341",
            Bgy = 0.3f,
            Fy = 0.475f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 4, PackageId = KamiyoModParameters.PackageId },
                new LorIdRoot { Id = 10, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public string MapName = "Hayate_Re21341";
        public bool MechChanging;
        public string MusicFileNamePhase2 = "HayatePhase2_Re21341.ogg";
        public string MusicFileNamePhase3 = "HayatePhase3_Re21341.ogg";
        public bool OneTurnCard;
        public int Phase;
        public int PhaseHp = 527;
        public int PhaseHp2 = 271;
        public string SaveDataId = "HayateSave21341";
        public LorId SpecialAttackCard = new LorId(KamiyoModParameters.PackageId, 904);
        public int SpecialCardStacks = 40;
        public bool WiltonCase;
        public override bool isImmortal => Phase < 3;

        public override void OnWaveStart()
        {
            if (_mapActive || _additionalUnit)
            {
                MapUtil.InitEnemyMap<Hayate_Re21341MapManager>(_cmh, MapModel);
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
            _buff = owner.CheckPermanentBuff<BattleUnitBuf_EntertainMe_Re21341>();
            if (_mapActive || _additionalUnit) _cmh.EnforceMap();
            owner.RemoveImmortalBuff();
            OneTurnCard = false;
            MechChanging = false;
        }

        public override void OnRoundEndTheLast()
        {
            if (_fingersnapSpecialTarget != null)
            {
                BattleObjectManager.instance.UnregisterUnit(_fingersnapSpecialTarget);
                _fingersnapSpecialTarget = null;
                UnitUtil.RefreshCombatUI();
            }

            if (Phase < 3 && owner.IsDead()) owner.UnitReviveAndRecovery(5, true);
            if (Phase < 1 && SpecialChangePhaseCondition()) MechChanging = true;
            if (Phase == 2 && !BattleObjectManager.instance.GetAliveList(owner.faction.ReturnOtherSideFaction()).Any())
                MechChanging = true;
            if (!MechChanging) return;
            Phase++;
            ChangePhase(Phase);
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (OneTurnCard || Phase > 2) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            if (Phase == 2 && _additionalUnit)
                origin = BattleDiceCardModel.CreatePlayingCard(
                    ItemXmlDataList.instance.GetCardItem(SpecialAttackCard));
            if (_buff.stack < SpecialCardStacks) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(AttackCard));
            OneTurnCard = true;
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override int SpeedDiceNumAdder()
        {
            return Phase == 0 ? 2 : Phase < 2 ? 5 : 3;
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (Phase < 1 && !MechChanging) return owner.MechHpCheck(dmg, PhaseHp, ref MechChanging);
            return Phase < 2 && !MechChanging
                ? owner.MechHpCheck(dmg, PhaseHp2, ref MechChanging)
                : base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 903:
                    _fingersnapSpecialTarget = curCard.target;
                    owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
                    break;
                case 904:
                    owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
                    break;
            }
        }

        public bool SpecialChangePhaseCondition()
        {
            var unit = BattleObjectManager.instance.GetAliveList(owner.faction.ReturnOtherSideFaction())
                .FirstOrDefault();
            return unit != null && unit.hp < unit.MaxHp * 0.7f;
        }

        public override void OnBattleEnd()
        {
            owner.OnEndBattleSave(SaveDataId, Phase);
        }

        public BattleUnitModel IgnoreSephiraSelectionTarget(LorId cardId)
        {
            if (!WiltonCase) return UnitUtil.IgnoreSephiraSelectionTarget(cardId == AttackCard);
            return BattleObjectManager.instance.GetAliveList(Faction.Player).LastOrDefault() ??
                   UnitUtil.IgnoreSephiraSelectionTarget(cardId == AttackCard);
        }

        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            var unit = IgnoreSephiraSelectionTarget(card.GetID());
            return unit ?? base.ChangeAttackTarget(card, idx);
        }

        private void ChangePhase(int phase, bool restart = false)
        {
            if (WiltonCase) return;
            switch (phase)
            {
                case 1:
                    owner.LevelUpEmotion(4 - owner.emotionDetail.EmotionLevel);
                    owner.ChangeCardCostByValue(-1, 99, true);
                    owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
                    for (var i = 1; i < 4; i++)
                        UnitUtil.AddOriginalPlayerUnit(i, 3);
                    UnitUtil.RefreshCombatUI();
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_CorruptedGodAuraRelease_Re21341>(ref EgoActive, dialog: EgoDialog,
                            color: Color.green);
                    _buff.stack = 40;
                    if (!_mapActive && !_additionalUnit) break;
                    CreatureFilter = true;
                    _cmh.SetMapBgm(MusicFileNamePhase2, true, MapName);
                    break;
                case 2:
                    owner.ChangeCardCostByValue(restart ? -2 : -1, 99, true);
                    _buff.stack = 0;
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_CorruptedGodAuraRelease_Re21341>(ref EgoActive, dialog: EgoDialog,
                            color: Color.green);
                    if (_mapActive || _additionalUnit)
                    {
                        CreatureFilter = true;
                        if (restart) _cmh.SetMapBgm(MusicFileNamePhase2, true, MapName);
                    }

                    break;
                case 3:
                    if (!_additionalUnit) break;
                    owner.ChangeCardCostByValue(-99, 99, true);
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_CorruptedGodAuraRelease_Re21341>(ref EgoActive, dialog: EgoDialog,
                            color: Color.green);
                    foreach (var unit in BattleObjectManager.instance.GetList(owner.faction.ReturnOtherSideFaction()))
                        BattleObjectManager.instance.UnregisterUnit(unit);
                    var kamiyoUnit = UnitUtil.AddNewUnitWithDefaultData(
                        new UnitModelRoot { PackageId = KamiyoModParameters.PackageId, Id = 10000901, UnitNameId = 4 },
                        0,
                        emotionLevel: owner.emotionDetail.EmotionLevel);
                    var kamiyoPassive = kamiyoUnit.GetActivePassive<PassiveAbility_AlterEgoPlayer_Re21341>();
                    if (kamiyoPassive != null)
                    {
                        kamiyoPassive.EgoDialog = new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "Kamiyo",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive4_Re21341"))
                                    .Value.Desc
                            }
                        };
                        kamiyoPassive.EgoActived();
                    }

                    UnitUtil.RefreshCombatUI();
                    kamiyoUnit.ChangeCardCostByValue(-5, 99, false);
                    kamiyoUnit.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 43));
                    var specialPassive =
                        kamiyoUnit.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 17));
                    specialPassive.OnWaveStart();
                    owner.UnitReviveAndRecovery(50, true);
                    kamiyoUnit.ApplyEmotionCards(_emotionCards);
                    if (!_mapActive) break;
                    CreatureFilter = false;
                    MapUtil.ActiveCreatureBattleCamFilterComponent(false);
                    _cmh.SetMapBgm(MusicFileNamePhase3, true, MapName);
                    break;
            }
        }

        public override void OnDie()
        {
            MapUtil.ActiveCreatureBattleCamFilterComponent(false);
        }

        public override void OnKill(BattleUnitModel target)
        {
            _emotionCards = UnitUtil.AddValueToEmotionCardList(UnitUtil.GetEmotionCardByUnit(target), _emotionCards);
            if (!WiltonCase) return;
            var playerUnit = BattleObjectManager.instance.GetAliveList(Faction.Player).FirstOrDefault();
            if (playerUnit != null)
            {
                playerUnit.forceRetreat = true;
                playerUnit.Die();
            }

            owner.DieFake();
        }

        public void SetWiltonCase(bool value)
        {
            WiltonCase = value;
        }
    }
}