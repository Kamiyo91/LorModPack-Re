using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using LOR_XML;
using Util_Re21341;
using Util_Re21341.BaseClass;
using Wilton_Re21341.Buffs;

namespace Wilton_Re21341.Passives
{
    public class PassiveAbility_KurosawaButlerEnemy_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtilBase _util;

        public override void OnWaveStart()
        {
            _util = new NpcMechUtilBase(new NpcMechUtilBaseModel
            {
                Owner = owner,
                Hp = 0,
                SetHp = 61,
                MechHp = 271,
                Counter = 0,
                MaxCounter = 5,
                Survive = true,
                HasEgo = true,
                HasMechOnHp = true,
                RecoverLightOnSurvive = true,
                ReloadMassAttackOnLethal = true,
                EgoType = typeof(BattleUnitBuf_Vengeance_Re21341),
                HasEgoAbDialog = true,
                HasSurviveAbDialog = true,
                SurviveAbDialogColor = AbColorType.Negative,
                EgoAbColorColor = AbColorType.Negative,
                SurviveAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "WiltonEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("WiltonEnemySurvive1_Re21341"))
                            .Value.Desc
                    }
                },
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "WiltonEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("WiltonEnemyEgoActive1_Re21341")).Value.Desc
                    }
                },
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 905)
            });
        }

        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_Vengeance_Re21341>()) return stack + 1;
            return stack;
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

        public override void OnRoundEnd()
        {
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
        }

        public void SetCountToMax()
        {
            _util.SetCounter(5);
        }

        public void ActiveMassAttackCount()
        {
            _util.SetMassAttack(true);
        }

        public void ForcedEgo()
        {
            _util.ForcedEgo();
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