using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Hayate_Re21341.Passives;
using KamiyoModPack.Util_Re21341.CommonBuffs;
using KamiyoModPack.Util_Re21341.CommonPassives;
using KamiyoModPack.Wilton_Re21341.Buffs;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Wilton_Re21341.Passives
{
    public class PassiveAbility_KurosawaButlerEnemy_Re21341 : PassiveAbilityBase
    {
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly bool _finalMech = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 6;
        private readonly bool _mapActive = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 11;

        private Wilton_Re21341MapManager _mapManager;
        public LorId AttackCard = new LorId(KamiyoModParameters.PackageId, 905);
        public int Counter;
        public bool CreatureFilter;
        public bool EgoActive;

        public List<AbnormalityCardDialog> EgoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "Wilton",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("WiltonEnemyEgoActive1_Re21341")).Value?.Desc ?? ""
            }
        };

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

        public int MaxCounter = 5;
        public bool MechChanging;
        public bool OneTurnCard;
        public int Phase;
        public int PhaseHp = 271;
        public string SaveDataId = "WiltonSave21341";
        public bool Survived;

        public List<AbnormalityCardDialog> SurviveDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "Wilton",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("WiltonEnemySurvive1_Re21341")).Value?.Desc ?? ""
            }
        };

        public override void OnWaveStart()
        {
            if (_mapActive || _finalMech)
            {
                MapUtil.InitEnemyMap<Wilton_Re21341MapManager>(_cmh, MapModel);
                _cmh.EnforceMap();
            }

            if (_finalMech)
                owner.AddStartBuffsToPlayerUnits(new List<BattleUnitBuf> { new BattleUnitBuf_Vip_Re21341() });
            Phase = NpcMechUtil.RestartPhase(SaveDataId);
            if (Phase != 0) ChangePhase(Phase, true);
        }

        public override void OnRoundStartAfter()
        {
            if (CreatureFilter) MapUtil.ActiveCreatureBattleCamFilterComponent();
        }

        public override void OnRoundStart()
        {
            if (_mapActive || _finalMech) _cmh.EnforceMap();
            owner.RemoveImmortalBuff();
            OneTurnCard = false;
            MechChanging = false;
            if (Phase == 0) return;
            Counter++;
            Mathf.Clamp(Counter, 0, MaxCounter);
        }

        public override void OnRoundEndTheLast()
        {
            if (_mapActive || _finalMech) CheckMapManager();
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
            return 2;
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (Phase < 1 && !MechChanging) return owner.MechHpCheck(dmg, PhaseHp, ref MechChanging);
            if (Phase >= 2) return base.BeforeTakeDamage(attacker, dmg);
            if (owner.SurviveCheck<BattleUnitBuf>(dmg, 0, ref Survived, dialog: SurviveDialog, color: Color.red))
                MechChanging = true;
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead()) return;
            if (Phase != 2 || !_finalMech) return;
            Phase++;
            ChangePhase(Phase);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 905:
                    owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
                    Counter = 0;
                    MapUtil.ChangeMapGeneric<Wilton_Re21341MapManager>(_cmh, MapModel);
                    break;
            }
        }

        public override void OnBattleEnd()
        {
            owner.OnEndBattleSave(SaveDataId, Phase);
            if (_finalMech)
                BattleObjectManager.instance.GetList(Faction.Player)
                    .FirstOrDefault(x => x.UnitData.unitData.bookItem.ClassInfo.id.id == 10000901)?.Revive(1);
        }

        private void ChangePhase(int phase, bool restart = false)
        {
            switch (phase)
            {
                case 1:
                    owner.ChangeCardCostByValue(-1, 99, true);
                    Counter = 5;
                    var unit = new UnitModelRoot
                    {
                        PackageId = KamiyoModParameters.PackageId,
                        Id = 9,
                        UnitNameId = 9
                    };
                    for (var i = 0; i < 3; i++)
                        UnitUtil.AddNewUnitWithDefaultData(unit,
                            BattleObjectManager.instance.GetList(owner.faction).Count,
                            emotionLevel: owner.emotionDetail.EmotionLevel,
                            unitSide: Faction.Enemy);
                    UnitUtil.RefreshCombatUI();
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_Vengeance_Re21341>(ref EgoActive, dialog: EgoDialog,
                            color: Color.red);
                    if ((!_mapActive && !_finalMech) || _mapManager == null) break;
                    CreatureFilter = true;
                    _mapManager.Phase = 1;
                    _mapManager.Update();
                    break;
                case 2:
                    owner.ChangeCardCostByValue(restart ? -2 : -1, 99, true);
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_Vengeance_Re21341>(ref EgoActive, dialog: EgoDialog,
                            color: Color.red);
                    CreatureFilter = false;
                    MapUtil.ActiveCreatureBattleCamFilterComponent(false);
                    Counter = 5;
                    break;
                case 3:
                    foreach (var playerUnit in BattleObjectManager.instance.GetAliveList(Faction.Player))
                        playerUnit.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_Vip_Re21341));
                    var hayate = UnitUtil.AddNewUnitWithDefaultData(new UnitModelRoot
                        {
                            PackageId = KamiyoModParameters.PackageId,
                            Id = 6,
                            UnitNameId = 6
                        }, 0,
                        emotionLevel: 5, unitSide: Faction.Enemy);
                    hayate.allyCardDetail.ExhaustAllCards();
                    hayate.allyCardDetail.AddNewCard(new LorId(KamiyoModParameters.PackageId, 903));
                    var passive =
                        hayate.passiveDetail.PassiveList.Find(x => x is PassiveAbility_HayateNpc_Re21341) as
                            PassiveAbility_HayateNpc_Re21341;
                    passive?.SetWiltonCase(true);
                    hayate.moveDetail.ReturnToFormationByBlink(true);
                    UnitUtil.RefreshCombatUI();
                    break;
            }
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x.passiveDetail.HasPassive<PassiveAbility_WillOTheWisp_Re21341>()))
                unit.Die();
        }

        private void CheckMapManager()
        {
            if (_mapManager == null &&
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject is Wilton_Re21341MapManager)
                _mapManager = SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject as Wilton_Re21341MapManager;
        }
    }
}