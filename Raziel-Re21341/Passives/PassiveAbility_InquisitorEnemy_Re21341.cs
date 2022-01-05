using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using LOR_XML;
using Raziel_Re21341.Buffs;
using Util_Re21341.BaseClass;

namespace Raziel_Re21341.Passives
{
    public class PassiveAbility_InquisitorEnemy_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtilBase _util;

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = 1,
                dmgRate = 25
            });
        }

        public override void OnWaveStart()
        {
            _util = new NpcMechUtilBase(new NpcMechUtilBaseModel
            {
                Owner = owner,
                Counter = 1,
                MaxCounter = 1,
                Survive = true,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_OwlSpiritNpc_Re21341),
                HasEgoAbDialog = true,
                MassAttackStartCount = true,
                EgoAbColorColor = AbColorType.Negative,
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "RazielEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("RazielEnemyEgoActive1_Re21341")).Value.Desc
                    }
                },
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 906)
            });
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
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