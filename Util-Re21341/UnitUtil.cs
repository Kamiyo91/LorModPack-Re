using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using HarmonyLib;
using LOR_DiceSystem;
using LOR_XML;
using TMPro;
using UI;
using UnityEngine;
using Util_Re21341.CommonBuffs;
using Random = UnityEngine.Random;

namespace Util_Re21341
{
    public static class UnitUtil
    {
        public static Faction ReturnOtherSideFaction(Faction faction)
        {
            return faction == Faction.Player ? Faction.Enemy : Faction.Player;
        }

        public static void PhaseChangeAllPlayerUnitRecoverBonus(int hp, int stagger, int light,
            bool fullLightRecover = false)
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
            {
                unit.RecoverHP(hp);
                unit.breakDetail.RecoverBreak(stagger);
                var finalLightRecover = fullLightRecover ? unit.cardSlotDetail.GetMaxPlayPoint() : light;
                unit.cardSlotDetail.RecoverPlayPoint(finalLightRecover);
            }
        }

        public static bool CantUseCardAfraid(BattleDiceCardModel card)
        {
            return card.XmlData.Spec.Ranged == CardRange.FarArea ||
                   card.XmlData.Spec.Ranged == CardRange.FarAreaEach || card.GetOriginCost() > 3 ||
                   card.XmlData.IsEgo() || card.XmlData.id.packageId == ModParameters.PackageId &&
                   (card.XmlData.id.id == 32 || card.XmlData.id.id == 33 || card.XmlData.id.id == 34 ||
                    card.XmlData.id.id == 35);
        }

        public static void DrawUntilX(BattleUnitModel owner, int x)
        {
            var count = owner.allyCardDetail.GetHand().Count;
            var num = x - count;
            if (num > 0) owner.allyCardDetail.DrawCards(num);
        }

        public static bool CheckSkinProjection(BattleUnitModel owner)
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin)) return true;
            if (owner.UnitData.unitData.bookItem == owner.UnitData.unitData.CustomBookItem) return false;
            owner.view.ChangeSkin(owner.UnitData.unitData.CustomBookItem.GetCharacterName());
            return true;
        }

        public static void VipDeathNpc(BattleUnitModel owner)
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x != owner))
                unit.Die();
        }

        public static void VipDeathPlayer()
        {
            StageController.Instance.GetCurrentStageFloorModel().Defeat();
            StageController.Instance.EndBattle();
        }

        public static void RemoveImmortalBuff(BattleUnitModel owner)
        {
            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_ImmortalUntilRoundEnd_Re21341) is
                BattleUnitBuf_ImmortalUntilRoundEnd_Re21341 buf)
                owner.bufListDetail.RemoveBuf(buf);
        }

        public static void RefreshCombatUI(bool forceReturn = false)
        {
            foreach (var (battleUnit, num) in BattleObjectManager.instance.GetList()
                         .Select((value, i) => (value, i)))
            {
                SingletonBehavior<UICharacterRenderer>.Instance.SetCharacter(battleUnit.UnitData.unitData, num, true);
                if (forceReturn)
                    battleUnit.moveDetail.ReturnToFormationByBlink(true);
            }

            BattleObjectManager.instance.InitUI();
        }

        public static void ChangeCardCostByValue(BattleUnitModel owner, int changeValue, int baseValue)
        {
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .Where(x => x.GetOriginCost() < baseValue))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(changeValue);
            }

            if (owner.faction == Faction.Enemy) owner.allyCardDetail.DrawCards(owner.UnitData.unitData.GetStartDraw());
        }

        public static bool CheckCardCost(BattleUnitModel owner, int baseValue)
        {
            return owner.allyCardDetail.GetAllDeck().Any(x => x.GetCost() > baseValue);
        }

        public static void UnitReviveAndRecovery(BattleUnitModel owner, int hp, bool recoverLight,
            bool skinChanged = false)
        {
            if (owner.IsDead())
            {
                owner.bufListDetail.GetActivatedBufList()
                    .RemoveAll(x => !x.CanRecoverHp(999) || !x.CanRecoverBreak(999));
                owner.Revive(hp);
                owner.moveDetail.ReturnToFormationByBlink(true);
                owner.view.EnableView(true);
                if (skinChanged)
                    CheckSkinProjection(owner);
                else
                    owner.view.CreateSkin();
            }
            else
            {
                owner.bufListDetail.GetActivatedBufList()
                    .RemoveAll(x => !x.CanRecoverHp(999) || !x.CanRecoverBreak(999));
                owner.RecoverHP(hp);
            }

            owner.bufListDetail.RemoveBufAll(BufPositiveType.Negative);
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_sealTemp));
            owner.breakDetail.ResetGauge();
            owner.breakDetail.nextTurnBreak = false;
            owner.breakDetail.RecoverBreakLife(1, true);
            if (recoverLight) owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
        }

        public static BattleUnitModel AddOriginalPlayerUnitPlayerSide(int index, int emotionLevel)
        {
            var allyUnit = Singleton<StageController>.Instance.CreateLibrarianUnit_fromBattleUnitData(index);
            allyUnit.OnWaveStart();
            allyUnit.allyCardDetail.DrawCards(allyUnit.UnitData.unitData.GetStartDraw());
            LevelUpEmotion(allyUnit, emotionLevel);
            allyUnit.cardSlotDetail.RecoverPlayPoint(allyUnit.cardSlotDetail.GetMaxPlayPoint());
            AddEmotionPassives(allyUnit);
            return allyUnit;
        }

        public static BattleUnitModel AddNewUnitEnemySide(UnitModel unit)
        {
            var unitWithIndex = Singleton<StageController>.Instance.AddNewUnit(Faction.Enemy,
                new LorId(ModParameters.PackageId, unit.Id), unit.Pos);
            LevelUpEmotion(unitWithIndex, unit.EmotionLevel);
            if (unit.LockedEmotion)
                unitWithIndex.emotionDetail.SetMaxEmotionLevel(unit.MaxEmotionLevel);
            unitWithIndex.cardSlotDetail.RecoverPlayPoint(unitWithIndex.cardSlotDetail.GetMaxPlayPoint());
            unitWithIndex.allyCardDetail.DrawCards(unitWithIndex.UnitData.unitData.GetStartDraw());
            unitWithIndex.formation = new FormationPosition(unitWithIndex.formation._xmlInfo);
            if (unit.AddEmotionPassive)
                AddEmotionPassives(unitWithIndex);
            if (unit.OnWaveStart)
                unitWithIndex.OnWaveStart();
            return unitWithIndex;
        }

        private static void AddEmotionPassives(BattleUnitModel unit)
        {
            var playerUnitsAlive = BattleObjectManager.instance.GetAliveList(Faction.Player);
            if (!playerUnitsAlive.Any()) return;
            foreach (var emotionCard in playerUnitsAlive.FirstOrDefault()
                         .emotionDetail.PassiveList.Where(x =>
                             x.XmlInfo.TargetType == EmotionTargetType.AllIncludingEnemy ||
                             x.XmlInfo.TargetType == EmotionTargetType.All))
            {
                if (unit.faction == Faction.Enemy &&
                    emotionCard.XmlInfo.TargetType == EmotionTargetType.All) continue;
                unit.emotionDetail.ApplyEmotionCard(emotionCard.XmlInfo);
            }
        }

        public static void ApplyEmotionCards(BattleUnitModel unit, IEnumerable<BattleEmotionCardModel> emotionCardList)
        {
            foreach (var card in emotionCardList) unit.emotionDetail.ApplyEmotionCard(card.XmlInfo);
        }

        public static IEnumerable<BattleEmotionCardModel> GetEmotionCardByUnit(BattleUnitModel unit)
        {
            return unit.emotionDetail.PassiveList.ToList();
        }

        public static void LevelUpEmotion(BattleUnitModel owner, int value)
        {
            for (var i = 0; i < value; i++)
            {
                owner.emotionDetail.LevelUp_Forcely(1);
                owner.emotionDetail.CheckLevelUp();
            }

            StageController.Instance.GetCurrentStageFloorModel().team.UpdateCoin();
        }

        public static BattleUnitModel AddNewUnitPlayerSide(StageLibraryFloorModel floor, UnitModel unit)
        {
            var unitData = new UnitDataModel(new LorId(ModParameters.PackageId, unit.Id), floor.Sephirah);
            unitData.SetCustomName(unit.Name);
            var allyUnit = BattleObjectManager.CreateDefaultUnit(Faction.Player);
            allyUnit.index = unit.Pos;
            allyUnit.grade = unitData.grade;
            allyUnit.formation = floor.GetFormationPosition(allyUnit.index);
            var unitBattleData = new UnitBattleDataModel(Singleton<StageController>.Instance.GetStageModel(), unitData);
            unitBattleData.Init();
            allyUnit.SetUnitData(unitBattleData);
            allyUnit.OnCreated();
            BattleObjectManager.instance.RegisterUnit(allyUnit);
            allyUnit.passiveDetail.OnUnitCreated();
            LevelUpEmotion(allyUnit, unit.EmotionLevel);
            if (unit.LockedEmotion)
                allyUnit.emotionDetail.SetMaxEmotionLevel(unit.MaxEmotionLevel);
            allyUnit.allyCardDetail.DrawCards(allyUnit.UnitData.unitData.GetStartDraw());
            allyUnit.cardSlotDetail.RecoverPlayPoint(allyUnit.cardSlotDetail.GetMaxPlayPoint());
            if (unit.AddEmotionPassive)
                AddEmotionPassives(allyUnit);
            allyUnit.OnWaveStart();
            return allyUnit;
        }

        public static void TestingUnitValuesImmortality()
        {
            var playerUnit = BattleObjectManager.instance.GetAliveList(Faction.Player);
            if (playerUnit == null) return;
            foreach (var unit in playerUnit)
            {
                if (unit.emotionDetail.EmotionLevel < 5)
                    for (var i = 0; i < 5; i++)
                    {
                        unit.emotionDetail.LevelUp_Forcely(1);
                        unit.emotionDetail.CheckLevelUp();
                    }

                if (!unit.bufListDetail.GetActivatedBufList()
                        .Exists(x => x is BattleUnitBuf_ImmortalForTestPurpose_Re21341))
                    unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalForTestPurpose_Re21341());
            }

            StageController.Instance.GetCurrentStageFloorModel().team.UpdateCoin();
        }

        public static void TestingUnitValuesBigDamage()
        {
            var playerUnit = BattleObjectManager.instance.GetAliveList(Faction.Player);
            if (playerUnit == null) return;
            foreach (var unit in playerUnit)
            {
                if (unit.emotionDetail.EmotionLevel < 5)
                    for (var i = 0; i < 5; i++)
                    {
                        unit.emotionDetail.LevelUp_Forcely(1);
                        unit.emotionDetail.CheckLevelUp();
                    }

                if (!unit.bufListDetail.GetActivatedBufList()
                        .Exists(x => x is BattleUnitBuf_BigDamageForTestingPurpose_Re21341))
                    unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_BigDamageForTestingPurpose_Re21341());
                unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalForTestPurpose_Re21341());
            }

            StageController.Instance.GetCurrentStageFloorModel().team.UpdateCoin();
        }

        public static void ReadyCounterCard(BattleUnitModel owner, int id)
        {
            var card = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id)));
            owner.cardSlotDetail.keepCard.AddBehaviours(card, card.CreateDiceCardBehaviorList());
            owner.allyCardDetail.ExhaustCardInHand(card);
        }

        public static void MakeEffect(BattleUnitModel unit, string path, float sizeFactor = 1f,
            BattleUnitModel target = null, float destroyTime = -1f)
        {
            try
            {
                SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect(path, sizeFactor, unit.view,
                    target?.view, destroyTime);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void SetPassiveCombatLog(PassiveAbilityBase passive, BattleUnitModel owner)
        {
            var battleCardResultLog = owner.battleCardResultLog;
            battleCardResultLog?.SetPassiveAbility(passive);
        }

        public static void AddCustomUnits(StageLibraryFloorModel instance, StageModel stage,
            List<UnitBattleDataModel> unitList, int dictionaryId)
        {
            if (!ModParameters.PreBattleUnits.TryGetValue(dictionaryId, out var stageParameters)) return;
            if (!stageParameters.Exists(x => x.SephirahUnit == instance.Sephirah)) return;
            foreach (var unitParameters in stageParameters)
            {
                var unitDataModel = new UnitDataModel(new LorId(ModParameters.PackageId, unitParameters.UnitId),
                    instance.Sephirah, true);
                unitDataModel.SetTemporaryPlayerUnitByBook(new LorId(ModParameters.PackageId,
                    unitParameters.UnitId));
                unitDataModel.bookItem.ClassInfo.categoryList.Add(BookCategory.DeckFixed);
                unitDataModel.isSephirah = false;
                unitDataModel.SetCustomName(ModParameters.NameTexts
                    .FirstOrDefault(x => x.Key == unitParameters.UnitNameId).Value);
                if (unitParameters.PassiveIds.Any())
                    foreach (var passive in unitParameters.PassiveIds)
                        unitDataModel.bookItem.ClassInfo.EquipEffect.PassiveList.Add(passive);
                unitDataModel.CreateDeckByDeckInfo();
                unitDataModel.forceItemChangeLock = true;
                if (!string.IsNullOrEmpty(unitParameters.SkinName))
                    unitDataModel.bookItem.ClassInfo.CharacterSkin = new List<string> { unitParameters.SkinName };
                var unitBattleDataModel = new UnitBattleDataModel(stage, unitDataModel);
                unitBattleDataModel.Init();
                unitList.Add(unitBattleDataModel);
            }
        }

        public static void BattleAbDialog(BattleDialogUI instance, List<AbnormalityCardDialog> dialogs,
            AbColorType colorType)
        {
            var component = instance.GetComponent<CanvasGroup>();
            var dialog = dialogs[Random.Range(0, dialogs.Count)].dialog;
            var txtAbnormalityDlg = (TextMeshProUGUI)typeof(BattleDialogUI).GetField("_txtAbnormalityDlg",
                AccessTools.all)?.GetValue(instance);
            txtAbnormalityDlg.text = dialog;
            if (colorType == AbColorType.Positive)
            {
                txtAbnormalityDlg.fontMaterial.SetColor("_GlowColor",
                    SingletonBehavior<BattleManagerUI>.Instance.positiveCoinColor);
                txtAbnormalityDlg.color = SingletonBehavior<BattleManagerUI>.Instance.positiveTextColor;
            }
            else
            {
                txtAbnormalityDlg.fontMaterial.SetColor("_GlowColor",
                    SingletonBehavior<BattleManagerUI>.Instance.negativeCoinColor);
                txtAbnormalityDlg.color = SingletonBehavior<BattleManagerUI>.Instance.negativeTextColor;
            }

            var canvas = (Canvas)typeof(BattleDialogUI).GetField("_canvas",
                AccessTools.all)?.GetValue(instance);
            canvas.enabled = true;
            component.interactable = true;
            component.blocksRaycasts = true;
            txtAbnormalityDlg.GetComponent<AbnormalityDlgEffect>().Init();
            var _ = (Coroutine)typeof(BattleDialogUI).GetField("_routine",
                AccessTools.all)?.GetValue(instance);
            var method = typeof(BattleDialogUI).GetMethod("AbnormalityDlgRoutine", AccessTools.all);
            instance.StartCoroutine(method.Invoke(instance, Array.Empty<object>()) as IEnumerator);
        }

        public static List<int> GetSamuraiGhostIndex(int originalUnitIndex)
        {
            switch (originalUnitIndex)
            {
                case 0:
                    return new List<int> { 1, 2, 3 };
                case 1:
                    return new List<int> { 0, 2, 3 };
                case 2:
                    return new List<int> { 0, 1, 3 };
                default:
                    return new List<int> { 0, 1, 2 };
            }
        }

        private static void SetBaseKeywordCard(LorId id, ref Dictionary<LorId, DiceCardXmlInfo> cardDictionary,
            ref List<DiceCardXmlInfo> cardXmlList)
        {
            var keywordsList = GetKeywordsList(id.id).ToList();
            var diceCardXmlInfo2 = CardOptionChange(cardDictionary[id], new List<CardOption>(), true, keywordsList);
            cardDictionary[id] = diceCardXmlInfo2;
            cardXmlList.Add(diceCardXmlInfo2);
        }

        private static void SetCustomCardOption(CardOption option, LorId id, bool keywordsRequired,
            ref Dictionary<LorId, DiceCardXmlInfo> cardDictionary, ref List<DiceCardXmlInfo> cardXmlList)
        {
            var keywordsList = new List<string>();
            if (keywordsRequired) keywordsList = GetKeywordsList(id.id).ToList();
            var diceCardXmlInfo2 = CardOptionChange(cardDictionary[id], new List<CardOption> { option },
                keywordsRequired,
                keywordsList);
            cardDictionary[id] = diceCardXmlInfo2;
            cardXmlList.Add(diceCardXmlInfo2);
        }

        private static List<int> GetAllOnlyCardsId()
        {
            var onlyPageCardList = new List<int>();
            foreach (var cardIds in ModParameters.OnlyCardKeywords.Select(x => x.Item2))
                onlyPageCardList.AddRange(cardIds);
            return onlyPageCardList;
        }

        private static IEnumerable<string> GetKeywordsList(int id)
        {
            var keyword = ModParameters.OnlyCardKeywords.FirstOrDefault(x => x.Item2.Contains(id))?.Item1;
            return string.IsNullOrEmpty(keyword)
                ? new List<string> { "LoRModPage_Re21341" }
                : new List<string> { "LoRModPage_Re21341", keyword };
        }

        private static DiceCardXmlInfo CardOptionChange(DiceCardXmlInfo cardXml, List<CardOption> option,
            bool keywordRequired, List<string> keywords,
            string skinName = "", string mapName = "", int skinHeight = 0)
        {
            return new DiceCardXmlInfo(cardXml.id)
            {
                workshopID = cardXml.workshopID,
                workshopName = cardXml.workshopName,
                Artwork = cardXml.Artwork,
                Chapter = cardXml.Chapter,
                category = cardXml.category,
                DiceBehaviourList = cardXml.DiceBehaviourList,
                _textId = cardXml._textId,
                optionList = option.Any() ? option : cardXml.optionList,
                Priority = cardXml.Priority,
                Rarity = cardXml.Rarity,
                Script = cardXml.Script,
                ScriptDesc = cardXml.ScriptDesc,
                Spec = cardXml.Spec,
                SpecialEffect = cardXml.SpecialEffect,
                SkinChange = string.IsNullOrEmpty(skinName) ? cardXml.SkinChange : skinName,
                SkinChangeType = cardXml.SkinChangeType,
                SkinHeight = skinHeight != 0 ? skinHeight : cardXml.SkinHeight,
                MapChange = string.IsNullOrEmpty(mapName) ? cardXml.MapChange : mapName,
                PriorityScript = cardXml.PriorityScript,
                Keywords = keywordRequired ? keywords : cardXml.Keywords
            };
        }

        public static void ChangeCardItem(ItemXmlDataList instance)
        {
            var dictionary = (Dictionary<LorId, DiceCardXmlInfo>)instance.GetType()
                .GetField("_cardInfoTable", AccessTools.all).GetValue(instance);
            var list = (List<DiceCardXmlInfo>)instance.GetType()
                .GetField("_cardInfoList", AccessTools.all).GetValue(instance);
            var onlyPageCardList = GetAllOnlyCardsId();
            foreach (var (key, _) in dictionary.Where(x => x.Key.packageId == ModParameters.PackageId).ToList())
            {
                if (ModParameters.PersonalCardList.Contains(key.id))
                {
                    SetCustomCardOption(CardOption.Personal, key, false, ref dictionary, ref list);
                    continue;
                }

                if (ModParameters.EgoPersonalCardList.Contains(key.id))
                {
                    SetCustomCardOption(CardOption.EgoPersonal, key, false, ref dictionary, ref list);
                    continue;
                }

                if (ModParameters.NoInventoryCardList.Contains(key.id))
                {
                    SetCustomCardOption(CardOption.NoInventory, key, false, ref dictionary, ref list);
                    continue;
                }

                if (onlyPageCardList.Contains(key.id))
                {
                    SetCustomCardOption(CardOption.OnlyPage, key, true, ref dictionary, ref list);
                    continue;
                }

                SetBaseKeywordCard(key, ref dictionary, ref list);
            }
        }

        public static void ChangePassiveItem()
        {
            foreach (var passive in Singleton<PassiveXmlList>.Instance.GetDataAll().Where(passive =>
                         passive.id.packageId == ModParameters.PackageId &&
                         ModParameters.UntransferablePassives.Contains(passive.id.id)))
                passive.CanGivePassive = false;
        }
    }
}