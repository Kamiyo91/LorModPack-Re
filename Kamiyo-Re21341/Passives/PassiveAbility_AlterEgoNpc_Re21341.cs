using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using Kamiyo_Re21341.Buffs;
using LOR_XML;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_AlterEgoNpc_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtilBase _util;
        public override void Init(BattleUnitModel self)
        {
            UnitUtil.ReturnToTheOriginalSkin(self, "KamiyoNormal_Re21341");
            base.Init(self);
        }
        public override void OnBattleEnd() => UnitUtil.ReturnToTheOriginalSkin(owner, "KamiyoNormal_Re21341");
        public override void OnWaveStart()
        {
            _util = new NpcMechUtilBase(new NpcMechUtilBaseModel
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
                    new AbnormalityCardDialog {id = "KamiyoEnemy", dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEnemySurvive1_Re21341")).Value}
                },
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog {id = "KamiyoEnemy", dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEnemyEgoActive1_Re21341")).Value},
                },
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 902)
            });
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
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
        }
        public void AddAdditionalPassive() => _util.AddAdditionalPassive();
        public void SetCountToMax() => _util.SetCounter(4);
        public void ActiveMassAttackCount() => _util.SetMassAttack(true);
        public void ForcedEgo() => _util.ForcedEgo();
        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }
        public override void OnDie() => UnitUtil.VipDeath(owner);

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard) => _util.OnUseCardResetCount(curCard.card.GetID());
    }
}
