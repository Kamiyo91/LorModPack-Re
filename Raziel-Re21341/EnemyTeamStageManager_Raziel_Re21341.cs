using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using LOR_XML;
using Raziel_Re21341.Passives;
using Util_Re21341;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Raziel_Re21341
{
    public class EnemyTeamStageManager_Raziel_Re21341 : EnemyTeamStageManager
    {
        private int _count;
        private BattleUnitModel _mainEnemyModel;
        private PassiveAbility_InquisitorEnemy_Re21341 _razielEnemyPassive;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Raziel_Re21341", new Raziel_Re21341MapManager(), false, true, 0.5f,
                0.375f, 0.5f, 0.225f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            UnitUtil.ChangeCardCostByValue(_mainEnemyModel, -2, 4);
            if (_mainEnemyModel != null)
                _razielEnemyPassive =
                    _mainEnemyModel.passiveDetail.PassiveList.Find(x => x is PassiveAbility_InquisitorEnemy_Re21341)
                        as
                        PassiveAbility_InquisitorEnemy_Re21341;
            _count = 0;
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }

        public override void OnRoundEndTheLast()
        {
            RazielIsDeadBeforeTurn10();
            CheckPhase();
            _count++;
        }

        private void CheckPhase()
        {
            switch (_count)
            {
                case 1:
                    _razielEnemyPassive.ForcedEgo();
                    UnitUtil.ChangeCardCostByValue(_mainEnemyModel, -4, 5);
                    _mainEnemyModel.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 41));
                    CustomMapHandler.SetMapBgm("RazielPhase2_Re21341.mp3", true, "Raziel_Re21341");
                    break;
                case 7:
                    _mainEnemyModel.forceRetreat = true;
                    _mainEnemyModel.Die();
                    break;
            }
        }

        private void RazielIsDeadBeforeTurn10()
        {
            if (_count > 6) return;
            if (!_mainEnemyModel.IsDead()) return;
            UnitUtil.UnitReviveAndRecovery(_mainEnemyModel, _mainEnemyModel.MaxHp, false);
            UnitUtil.BattleAbDialog(_mainEnemyModel.view.dialogUI,
                new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "RazielEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("RazielImmortal_Re21341")).Value.Desc
                    }
                }, AbColorType.Negative);
        }
    }
}