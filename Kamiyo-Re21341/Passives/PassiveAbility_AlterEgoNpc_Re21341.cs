using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using Kamiyo_Re21341.Buffs;
using Kamiyo_Re21341.MechUtil;
using LOR_XML;
using Util_Re21341;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_AlterEgoNpc_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtil_Kamiyo _util;

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "KamiyoNormal_Re21341" };
        }

        public override void OnWaveStart()
        {
            _util = new NpcMechUtil_Kamiyo(new NpcMechUtilBaseModel
            {
                Owner = owner,
                Hp = 0,
                SetHp = 161,
                MechHp = 100,
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
                EgoType = typeof(BattleUnitBuf_AlterEgoRelease_Re21341),
                AdditionalPassiveId = new LorId(ModParameters.PackageId, 11),
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
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 902)
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

        public void AddAdditionalPassive()
        {
            _util.AddAdditionalPassive();
        }

        public void SetCountToMax()
        {
            _util.SetCounter(4);
        }

        public void ActiveMassAttackCount()
        {
            _util.SetMassAttack(true);
        }

        public void ForcedEgo()
        {
            _util.ForcedEgo();
        }

        public void ForcedEgoRestart()
        {
            _util.TurnEgoAbDialogOff();
            _util.ForcedEgo();
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnDie()
        {
            UnitUtil.VipDeath(owner);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseCardResetCount(curCard);
        }
    }
}