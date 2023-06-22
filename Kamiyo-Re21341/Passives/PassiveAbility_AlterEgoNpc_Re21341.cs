using System.Collections.Generic;
using System.Linq;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Buffs;
using KamiyoModPack.Kamiyo_Re21341.MapManager;
using KamiyoModPack.Mio_Re21341.Passives;
using LOR_DiceSystem;
using LOR_XML;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Passives
{
    public class PassiveAbility_AlterEgoNpc_Re21341 : PassiveAbilityBase
    {
        private const string OriginalSkinName = "KamiyoNormal_Re21341";
        private const string EgoSkinName = "KamiyoMask_Re21341";

        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private readonly bool _mapActive = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 3;

        public LorId AttackCard = new LorId(KamiyoModParameters.PackageId, 902);
        public int Counter;
        public bool EgoActive;

        public List<AbnormalityCardDialog> EgoDialog = new List<AbnormalityCardDialog>
        {
            new AbnormalityCardDialog
            {
                id = "KamiyoEnemy",
                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId]?.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("KamiyoEnemyEgoActive1_Re21341")).Value?.Desc ?? ""
            }
        };

        public MapModelRoot MapModelPhase1 = new MapModelRoot
        {
            Component = "Kamiyo1_Re21341MapManager",
            Stage = "Kamiyo1_Re21341",
            Bgy = 0.2f,
            Fy = 0.45f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 3, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public MapModelRoot MapModelPhase2 = new MapModelRoot
        {
            Component = "Kamiyo2_Re21341MapManager",
            Stage = "Kamiyo2_Re21341",
            Bgy = 0.475f,
            Fy = 0.225f,
            OriginalMapStageIds = new List<LorIdRoot>
            {
                new LorIdRoot { Id = 3, PackageId = KamiyoModParameters.PackageId }
            }
        };

        public int MaxCounter = 5;
        public bool MechChanging;
        public bool OneTurnCard;
        public int Phase;
        public int PhaseHp = 161;
        public string SaveDataId = "KamiyoSave21341";

        public override void OnWaveStart()
        {
            if (_mapActive)
            {
                MapUtil.InitEnemyMap<Kamiyo1_Re21341MapManager>(_cmh, MapModelPhase1);
                MapUtil.InitEnemyMap<Kamiyo2_Re21341MapManager>(_cmh, MapModelPhase2);
                _cmh.EnforceMap();
            }

            owner.AddBuff<BattleUnitBuf_Shock_Re21341>(1);
            Phase = NpcMechUtil.RestartPhase(SaveDataId);
            if (Phase != 0) ChangePhase(Phase);
        }

        public override void OnRoundStart()
        {
            if (_mapActive) _cmh.EnforceMap(Phase == 0 ? 0 : 1);
            owner.RemoveImmortalBuff();
            OneTurnCard = false;
            MechChanging = false;
            if (Phase == 0) return;
            Counter++;
            Mathf.Clamp(Counter, 0, MaxCounter);
        }

        public override void OnRoundEndTheLast()
        {
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
            return Phase < 1 && !MechChanging
                ? owner.MechHpCheck(dmg, PhaseHp, ref MechChanging)
                : base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != KamiyoModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 902:
                    owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
                    Counter = 0;
                    MapUtil.ChangeMapGeneric<Kamiyo2_Re21341MapManager>(_cmh, MapModelPhase2);
                    break;
            }
        }

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { OriginalSkinName };
            owner.OnEndBattleSave(SaveDataId, Phase);
        }

        private void ChangePhase(int phase)
        {
            switch (phase)
            {
                case 1:
                    owner.LevelUpEmotion(4 - owner.emotionDetail.EmotionLevel);
                    owner.ChangeCardCostByValue(-1, 99, true);
                    owner.UnitReviveAndRecovery(owner.MaxHp, true);
                    Counter = 5;
                    SummonMioMemory();
                    if (!EgoActive)
                        owner.EgoActive<BattleUnitBuf_AlterEgoRelease_Re21341>(ref EgoActive, EgoSkinName, true,
                            false, null, EgoDialog, Color.red);
                    ChangeDiceEffects(owner);
                    var card = owner.allyCardDetail.GetAllDeck()
                        .FirstOrDefault(x => x.GetID() == new LorId(KamiyoModParameters.PackageId, 21));
                    owner.allyCardDetail.ExhaustACardAnywhere(card);
                    owner.allyCardDetail.AddNewCardToDeck(new LorId(KamiyoModParameters.PackageId, 22));
                    if (!owner.passiveDetail.HasPassive<PassiveAbility_MaskOfPerceptionNpc_Re21341>())
                        owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 11));
                    if (_mapActive) _cmh.EnforceMap(Phase == 0 ? 0 : 1);
                    break;
            }
        }

        private void SummonMioMemory()
        {
            var unit = new UnitModelRoot { PackageId = KamiyoModParameters.PackageId, Id = 5, UnitNameId = 5 };
            UnitUtil.AddNewUnitWithDefaultData(unit,
                BattleObjectManager.instance.GetList(owner.faction.ReturnOtherSideFaction()).Count, true, 4,
                Faction.Enemy);
        }

        public override int GetMaxHpBonus()
        {
            return Phase != 0 ? 314 : 0;
        }

        public override int GetMaxBpBonus()
        {
            return Phase != 0 ? 173 : 0;
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x.passiveDetail.HasPassive<PassiveAbility_MioMemory_Re21341>()))
                unit.Die();
        }

        public static void ChangeDiceEffects(BattleUnitModel owner)
        {
            foreach (var card in owner.allyCardDetail.GetAllDeck())
            {
                card.CopySelf();
                foreach (var dice in card.GetBehaviourList())
                    ChangeCardDiceEffect(dice);
            }
        }

        private static void ChangeCardDiceEffect(DiceBehaviour dice)
        {
            switch (dice.EffectRes)
            {
                case "KamiyoHit_Re21341":
                    dice.EffectRes = "KamiyoHitEgo_Re21341";
                    break;
                case "KamiyoSlash_Re21341":
                    dice.EffectRes = "KamiyoSlashEgo_Re21341";
                    break;
                case "PierceKamiyo_Re21341":
                    dice.EffectRes = "PierceKamiyoMask_Re21341";
                    break;
            }
        }
    }
}