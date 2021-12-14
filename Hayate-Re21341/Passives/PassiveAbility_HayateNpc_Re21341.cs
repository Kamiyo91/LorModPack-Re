using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using Hayate_Re21341.Buffs;
using Hayate_Re21341.MechUtil;
using LOR_XML;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Hayate_Re21341.Passives
{
    public class PassiveAbility_HayateNpc_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtil_Hayate _util;
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
                    new AbnormalityCardDialog {id = "HayateEnemy", dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("HayateEnemyEgoActive1_Re21341")).Value.Desc},
                },
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 903),
                SecondaryMechCard = new LorId(ModParameters.PackageId, 904)
            });
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
        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnDieOtherUnit(BattleUnitModel unit)
        {
            if (unit.faction == Faction.Player && Singleton<StageController>.Instance
                    .EnemyStageManager is EnemyTeamStageManager_Hayate_Re21341 manager)
            {
                manager.AddValueToEmotionCardList(UnitUtil.GetEmotionCardByUnit(unit));
            }
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
        public void ForcedEgo() => _util.ForcedEgo();
        public override void OnRoundEndTheLast() => _util.DeleteTarget();

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard) => _util.OnUseCardResetCount(curCard);
    }
}
