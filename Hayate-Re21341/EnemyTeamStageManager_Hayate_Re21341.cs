using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using CustomMapUtility;
using Kamiyo_Re21341.Passives;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.CommonBuffs;
using KamiyoStaticUtil.Utils;
using LOR_XML;

namespace Hayate_Re21341
{
    public class EnemyTeamStageManager_Hayate_Re21341 : EnemyTeamStageManager
    {
        private readonly List<BattleEmotionCardModel> _emotionCards = new List<BattleEmotionCardModel>();

        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();

        private bool _allySummon;
        private bool _finalMech;
        private bool _lastPhaseStarted;
        private BattleUnitModel _mainEnemyModel;
        private bool _phaseChanged;
        private BattleUnitModel _sephiraModel;

        public override void OnWaveStart()
        {
            _finalMech = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 4;
            CustomMapHandler.InitCustomMap("Hayate_Re21341", typeof(Hayate_Re21341MapManager), false, true, 0.5f, 0.3f,
                0.5f, 0.475f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _sephiraModel = BattleObjectManager.instance.GetList(Faction.Player).FirstOrDefault();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            _phaseChanged = false;
            _lastPhaseStarted = false;
            _allySummon = false;
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
            CheckUnitSummon();
            CheckLastPhase();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }

        public override void OnRoundStart_After()
        {
            if (_phaseChanged) MapStaticUtil.ActiveCreatureBattleCamFilterComponent();
        }

        private void CheckPhase()
        {
            if (_mainEnemyModel.hp > 527 || _phaseChanged) return;
            _phaseChanged = true;
            MapStaticUtil.ActiveCreatureBattleCamFilterComponent();
            CustomMapHandler.SetMapBgm("HayatePhase2_Re21341.ogg", true, "Hayate_Re21341");
        }

        private BattleUnitModel PrepareAllyUnit()
        {
            var allyUnit = UnitUtil.AddNewUnitPlayerSide(_floor, new UnitModel
            {
                Id = 10000901,
                Name = ModParameters.NameTexts.FirstOrDefault(x => x.Key.Equals("4")).Value,
                EmotionLevel = 5,
                Pos = 0,
                Sephirah = _floor.Sephirah
            }, KamiyoModParameters.PackageId);
            return allyUnit;
        }

        private void CheckUnitSummon()
        {
            if (_allySummon || BattleObjectManager.instance.GetAliveList(Faction.Player).Count < 1 ||
                _sephiraModel.hp > _sephiraModel.MaxHp * 0.75f && !_phaseChanged) return;
            _allySummon = true;
            for (var i = 1; i < 4; i++)
            {
                var unit = UnitUtil.AddOriginalPlayerUnitPlayerSide(i, _sephiraModel.emotionDetail.EmotionLevel);
                UnitUtil.BattleAbDialog(unit.view.dialogUI,
                    new List<AbnormalityCardDialog>
                    {
                        new AbnormalityCardDialog
                        {
                            id = "HayateEnemy",
                            dialog = ModParameters.EffectTexts
                                .FirstOrDefault(x => x.Key.Equals($"HayateBattleAllyEntry{i}_Re21341")).Value.Desc
                        }
                    }, AbColorType.Negative);
            }

            UnitUtil.RefreshCombatUI();
        }

        private void CheckLastPhase()
        {
            if (!_finalMech || _lastPhaseStarted || !_phaseChanged || _mainEnemyModel.hp > 100 ||
                BattleObjectManager.instance.GetAliveList(Faction.Player).Count > 0) return;
            _lastPhaseStarted = true;
            CustomMapHandler.SetMapBgm("HayatePhase3_Re21341.ogg", true, "Hayate_Re21341");
            foreach (var unit in BattleObjectManager.instance.GetList(Faction.Player))
                BattleObjectManager.instance.UnregisterUnit(unit);
            var allyUnit = PrepareAllyUnit();
            UnitUtil.RefreshCombatUI();
            var specialPassive = allyUnit.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 17));
            allyUnit.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 43));
            var passive = allyUnit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_AlterEgoPlayer_Re21341) as
                PassiveAbility_AlterEgoPlayer_Re21341;
            specialPassive.OnWaveStart();
            passive?.ForcedEgo();
            passive?.SetDieAtEnd();
            UnitUtil.ChangeCardCostByValue(allyUnit, -5, 6);
            UnitUtil.ApplyEmotionCards(allyUnit, _emotionCards);
            _mainEnemyModel.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_KamiyoImmortal));
            UnitUtil.UnitReviveAndRecovery(_mainEnemyModel, 50, true);
            UnitUtil.BattleAbDialog(_mainEnemyModel.view.dialogUI,
                new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Hayate",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("HayateEnemyFinalPhase1_Re21341")).Value.Desc
                    }
                }, AbColorType.Negative);
        }

        public void AddValueToEmotionCardList(IEnumerable<BattleEmotionCardModel> card)
        {
            _emotionCards.AddRange(card.Where(emotionCard =>
                !_emotionCards.Exists(x => x.XmlInfo.Equals(emotionCard.XmlInfo))));
        }
    }
}