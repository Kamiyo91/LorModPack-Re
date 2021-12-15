using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
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

        public static bool CheckSkinProjection(BattleUnitModel owner)
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem == owner.UnitData.unitData.CustomBookItem) return false;
            owner.view.ChangeSkin(owner.UnitData.unitData.CustomBookItem.GetCharacterName());
            return true;
        }
        public static void VipDeath(BattleUnitModel owner)
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x != owner))
            {
                unit.Die();
            }
        }
        public static void ReturnToTheOriginalSkin(BattleUnitModel owner, string charName)
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { charName };
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
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().Where(x => x.GetOriginCost() < baseValue))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(changeValue);
            }
        }
        public static void UnitReviveAndRecovery(BattleUnitModel owner, int hp,bool recoverLight)
        {
            if (owner.IsDead()) owner.Revive(hp);
            else owner.RecoverHP(hp);
            owner.bufListDetail.RemoveBufAll(BufPositiveType.Negative);
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_sealTemp));
            owner.breakDetail.ResetGauge();
            owner.breakDetail.nextTurnBreak = false;
            owner.breakDetail.RecoverBreakLife(1, true);
            if(recoverLight)owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
        }

        public static BattleUnitModel AddOriginalPlayerUnitPlayerSide(int index, int emotionLevel)
        {
            var allyUnit = Singleton<StageController>.Instance.CreateLibrarianUnit_fromBattleUnitData(index);
            allyUnit.OnWaveStart();
            allyUnit.allyCardDetail.DrawCards(allyUnit.UnitData.unitData.GetStartDraw());
            allyUnit.emotionDetail.SetEmotionLevel(emotionLevel);
            allyUnit.emotionDetail.Reset();
            allyUnit.cardSlotDetail.RecoverPlayPoint(allyUnit.cardSlotDetail.GetMaxPlayPoint());
            AddEmotionPassives(allyUnit);
            return allyUnit;
        }

        public static BattleUnitModel AddNewUnitEnemySide(UnitModel unit)
        {
            var unitWithIndex = Singleton<StageController>.Instance.AddNewUnit(Faction.Enemy,
                new LorId(ModParameters.PackageId, unit.Id), unit.Pos);
            unitWithIndex.emotionDetail.SetEmotionLevel(unit.EmotionLevel);
            if (unit.LockedEmotion)
                unitWithIndex.emotionDetail.SetMaxEmotionLevel(unit.MaxEmotionLevel);
            else
                unitWithIndex.emotionDetail.Reset();
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
            allyUnit.emotionDetail.SetEmotionLevel(unit.EmotionLevel);
            if (unit.LockedEmotion)
                allyUnit.emotionDetail.SetMaxEmotionLevel(unit.MaxEmotionLevel);
            else
                allyUnit.emotionDetail.Reset();
            allyUnit.allyCardDetail.DrawCards(allyUnit.UnitData.unitData.GetStartDraw());
            allyUnit.cardSlotDetail.RecoverPlayPoint(allyUnit.cardSlotDetail.GetMaxPlayPoint());
            if (unit.AddEmotionPassive)
                AddEmotionPassives(allyUnit);
            allyUnit.OnWaveStart();
            return allyUnit;
        }
        public static void PrepareSephirahSkin(BattleUnitModel owner, int id, string charName,ref BattleDialogueModel dlg)
        {
            dlg = owner.UnitData.unitData.battleDialogModel;
            owner.UnitData.unitData.SetTempName(charName);
            RefreshCombatUI();
            owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(ModParameters.PackageId, id));
        }
        public static void ReturnToTheOriginalBaseSkin(BattleUnitModel owner, BattleDialogueModel dlg)
        {
            owner.UnitData.unitData.ResetTempName();
            owner.UnitData.unitData.battleDialogModel = dlg;
        }

        public static void TestingUnitValues()
        {
            var playerUnit = BattleObjectManager.instance.GetAliveList(Faction.Player);
            if (playerUnit == null) return;
            foreach (var unit in playerUnit)
            {
                if (!unit.bufListDetail.GetActivatedBufList()
                        .Exists(x => x is BattleUnitBuf_ImmortalForTestPurpose_Re21341))
                    unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalForTestPurpose_Re21341());
                unit.emotionDetail.SetEmotionLevel(5);
            }
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

        public static void AddUnitSephiraOnly(StageLibraryFloorModel instance, StageModel stage, UnitDataModel data)
        {
            var list = (List<UnitBattleDataModel>)instance.GetType().GetField("_unitList", AccessTools.all)
                ?.GetValue(instance);
            var unitBattleDataModel = new UnitBattleDataModel(stage, data);
            if (!unitBattleDataModel.unitData.isSephirah) return;
            unitBattleDataModel.Init();
            list?.Add(unitBattleDataModel);
        }

        public static void ClearCharList(StageLibraryFloorModel instance)
        {
            var list = (List<UnitBattleDataModel>)instance.GetType().GetField("_unitList", AccessTools.all)
                ?.GetValue(instance);
            list?.Clear();
        }

        public static void FillBaseUnit(StageLibraryFloorModel floor)
        {
            var modelTeam = (List<UnitBattleDataModel>)typeof(StageLibraryFloorModel).GetField("_unitList",
                    AccessTools.all)
                ?.GetValue(Singleton<StageController>.Instance.GetStageModel().GetFloor(floor.Sephirah));
            var stage = Singleton<StageController>.Instance.GetStageModel();
            modelTeam?.AddRange(floor._floorModel.GetUnitDataList()
                .Where(x => x.OwnerSephirah == floor.Sephirah && !x.isSephirah)
                .Select(unit => InitUnitDefault(stage, unit)));
        }

        private static UnitBattleDataModel InitUnitDefault(StageModel stage, UnitDataModel data)
        {
            var unitBattleDataModel = new UnitBattleDataModel(stage, data);
            unitBattleDataModel.Init();
            return unitBattleDataModel;
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
            var onlyPageCardList = new List<int>(ModParameters.HayateCardList.Count +
                                                 ModParameters.KamiyoCardList.Count + ModParameters.MioCardList.Count +
                                                 ModParameters.SamuraiCardList.Count);
            onlyPageCardList.AddRange(ModParameters.HayateCardList);
            onlyPageCardList.AddRange(ModParameters.MioCardList);
            onlyPageCardList.AddRange(ModParameters.KamiyoCardList);
            onlyPageCardList.AddRange(ModParameters.SamuraiCardList);
            return onlyPageCardList;
        }

        private static IEnumerable<string> GetKeywordsList(int id)
        {
            if (ModParameters.KamiyoCardList.Contains(id))
                return new List<string> { "LoRModPage_Re21341", "KamiyoPage_Re21341" };
            if (ModParameters.MioCardList.Contains(id))
                return new List<string> { "LoRModPage_Re21341", "MioPage_Re21341" };
            if (ModParameters.HayateCardList.Contains(id))
                return new List<string> { "LoRModPage_Re21341", "HayatePage_Re21341" };
            return ModParameters.SamuraiCardList.Contains(id)
                ? new List<string> { "LoRModPage_Re21341", "SamuraiPage_Re21341" }
                : new List<string> { "LoRModPage_Re21341" };
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
            foreach (var passive in Singleton<PassiveXmlList>.Instance.GetDataAll().Where(passive => passive.id.packageId == ModParameters.PackageId &&
                         ModParameters.UntransferablePassives.Contains(passive.id.id)))
            {
                passive.CanGivePassive = false;
            }
        }
    }
}