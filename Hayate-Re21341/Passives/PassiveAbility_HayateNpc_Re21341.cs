using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using Hayate_Re21341.Buffs;
using Hayate_Re21341.MechUtil;
using LOR_XML;
using Util_Re21341;
using Util_Re21341.CommonBuffs;

namespace Hayate_Re21341.Passives
{
    public class PassiveAbility_HayateNpc_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtil_Hayate _util;
        private bool _wiltonCase;

        public override void OnWaveStart()
        {
            _util = new NpcMechUtil_Hayate(new NpcMechUtil_HayateModel
            {
                Owner = owner,
                MechHp = 527,
                SecondMechHp = 100,
                HasEgo = true,
                HasMechOnHp = true,
                SecondMechHpExist = true,
                RefreshUI = true,
                MassAttackStartCount = true,
                EgoType = typeof(BattleUnitBuf_TrueGodAuraRelease_Re21341),
                HasEgoAbDialog = true,
                HasSurviveAbDialog = true,
                EgoAbColorColor = AbColorType.Positive,
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "HayateEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("HayateEnemyEgoActive1_Re21341")).Value.Desc
                    }
                },
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 903),
                SecondaryMechCard = new LorId(ModParameters.PackageId, 904),
                FinalMech = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 4
            });
            _wiltonCase = false;
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_Immortal_Re21341());
            if (!_util.GetFinalMechValue()) owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_Immortal_Re21341));
        }

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.SecondMechHpCheck(dmg);
            _util.MechHpCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnStartBattle()
        {
            UnitUtil.RemoveImmortalBuff(owner);
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.HayateIsDeadBeforePhase3();
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnDieOtherUnit(BattleUnitModel unit)
        {
            if (unit.faction == Faction.Player && Singleton<StageController>.Instance
                    .EnemyStageManager is EnemyTeamStageManager_Hayate_Re21341 manager)
                manager.AddValueToEmotionCardList(UnitUtil.GetEmotionCardByUnit(unit));
        }

        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            var unit = _util.ChooseEgoAttackTarget(card.GetID());
            return unit ?? base.ChangeAttackTarget(card, idx);
        }

        public override void OnRoundEnd()
        {
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
        }

        public override void OnRoundEndTheLast()
        {
            _util.DeleteTarget();
            _util.CheckPhase();
        }

        public void SetWiltonCaseOn()
        {
            _wiltonCase = true;
        }

        public override void OnKill(BattleUnitModel target)
        {
            if (!_wiltonCase) return;
            var unit = BattleObjectManager.instance.GetAliveList(Faction.Player).FirstOrDefault();
            if (unit != null)
            {
                unit.forceRetreat = true;
                unit.Die();
            }

            owner.DieFake();
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseCardResetCount(curCard);
        }
    }
}