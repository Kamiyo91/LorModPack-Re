using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models;
using Kamiyo_Re21341.Buffs;
using Kamiyo_Re21341.MapManager;
using Kamiyo_Re21341.MechUtil;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_AlterEgoNpc_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtil_Kamiyo _util;

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "KamiyoNormal_Re21341" };
            _util.OnEndBattle();
        }

        public override void OnWaveStart()
        {
            var continueCheck = Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<bool>("PhaseKamiyoRe21341", out var curPhase) && curPhase;
            if (continueCheck) owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 11));
            _util = new NpcMechUtil_Kamiyo(new NpcMechUtil_KamiyoModel
            {
                Owner = owner,
                Hp = 0,
                SetHp = 161,
                MechHp = 161,
                Counter = 0,
                MaxCounter = 4,
                Survive = true,
                HasEgo = true,
                HasMechOnHp = true,
                HasAdditionalPassive = true,
                NearDeathBuffExist = true,
                RefreshUI = true,
                ReloadMassAttackOnLethal = true,
                RecoverLightOnSurvive = true,
                SkinName = "KamiyoMask_Re21341",
                EgoMapName = "Kamiyo2_Re21341",
                EgoMapType = typeof(Kamiyo2_Re21341MapManager),
                BgY = 0.475f,
                FlY = 0.225f,
                OriginalMapStageIds = new List<LorId>
                    { new LorId(KamiyoModParameters.PackageId, 3), new LorId(KamiyoModParameters.PackageId, 12) },
                EgoType = typeof(BattleUnitBuf_AlterEgoRelease_Re21341),
                AdditionalPassiveId = new LorId(KamiyoModParameters.PackageId, 11),
                NearDeathBuffType = typeof(BattleUnitBuf_NearDeathNpc_Re21341),
                HasEgoAbDialog = true,
                HasSurviveAbDialog = true,
                SurviveAbDialogColor = AbColorType.Negative,
                EgoAbColorColor = AbColorType.Negative,
                SurviveAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "KamiyoEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("KamiyoEnemySurvive1_Re21341")).Value.Desc
                    }
                },
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "KamiyoEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("KamiyoEnemyEgoActive1_Re21341")).Value.Desc
                    }
                },
                LorIdEgoMassAttack = new LorId(KamiyoModParameters.PackageId, 902),
                EgoAttackCardId = new LorId(KamiyoModParameters.PackageId, 902),
                PhaseChanged = continueCheck,
                Restart = continueCheck
            });
        }

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.MechHpCheck(dmg);
            _util.SurviveCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnStartBattle()
        {
            UnitUtil.RemoveImmortalBuff(owner);
        }

        public override void OnRoundStart()
        {
            _util.CheckRestart();
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>()) return;
            SkinUtil.BurnEffect(owner);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 3, owner);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, owner);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, owner);
        }

        public override void OnRoundEnd()
        {
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
        }

        public override void OnRoundEndTheLast()
        {
            _util.CheckPhase();
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList()
                         .Where(x => x.Book.BookId == new LorId(KamiyoModParameters.PackageId, 5)))
                unit.Die();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseCardResetCount(curCard);
            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }
    }
}