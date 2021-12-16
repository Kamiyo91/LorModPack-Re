using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using LOR_XML;
using Mio_Re21341.Buffs;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragmentEnemy_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtilBase _util;

        public override void OnBattleEnd()
        {
            UnitUtil.ReturnToTheOriginalSkin(owner, "MioNormalEye_Re21341");
        }

        public override void OnWaveStart()
        {
            UnitUtil.ReturnToTheOriginalSkin(owner, "MioNormalEye_Re21341",true);
            _util = new NpcMechUtilBase(new NpcMechUtilBaseModel
            {
                Owner = owner,
                Hp = 0,
                SetHp = 179,
                MechHp = 271,
                Counter = 0,
                MaxCounter = 4,
                Survive = true,
                HasEgo = true,
                HasMechOnHp = true,
                RecoverLightOnSurvive = true,
                RefreshUI = true,
                ReloadMassAttackOnLethal = true,
                SkinName = "MioRedEye_Re21341",
                EgoType = typeof(BattleUnitBuf_CorruptedGodAuraRelease_Re21341),
                HasEgoAbDialog = true,
                HasSurviveAbDialog = true,
                SurviveAbDialogColor = AbColorType.Negative,
                EgoAbColorColor = AbColorType.Negative,
                SurviveAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "MioEnemy",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("MioEnemySurvive1_Re21341"))
                            .Value.Desc
                    }
                },
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "MioEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("MioEnemyEgoActive1_Re21341")).Value.Desc
                    }
                },
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 900)
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

        public override void OnRoundEnd()
        {
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
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