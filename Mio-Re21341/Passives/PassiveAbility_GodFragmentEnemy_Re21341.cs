using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;
using Mio_Re21341.Buffs;
using Mio_Re21341.MechUtil;

namespace Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragmentEnemy_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtil_Mio _util;

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "MioNormalEye_Re21341" };
            _util.OnEndBattle();
        }

        public override void OnWaveStart()
        {
            _util = new NpcMechUtil_Mio(new NpcMechUtilBaseModel
            {
                Owner = owner,
                Hp = 0,
                SetHp = 179,
                MechHp = 271,
                Counter = 0,
                MaxCounter = 4,
                SpecialCardCost = 2,
                Survive = true,
                HasEgo = true,
                HasMechOnHp = true,
                RecoverLightOnSurvive = true,
                RefreshUI = true,
                ReloadMassAttackOnLethal = true,
                SkinName = "MioRedEye_Re21341",
                EgoMapName = "Mio_Re21341",
                EgoMapType = typeof(Mio_Re21341MapManager),
                BgY = 0.2f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(KamiyoModParameters.PackageId, 2), new LorId(KamiyoModParameters.PackageId, 9),
                    new LorId(KamiyoModParameters.PackageId, 12)
                },
                EgoType = typeof(BattleUnitBuf_CorruptedGodAuraRelease_Re21341),
                SpecialBufType = typeof(BattleUnitBuf_SakuraPetal_Re21341),
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
                LorIdEgoMassAttack = new LorId(KamiyoModParameters.PackageId, 900),
                EgoAttackCardId = new LorId(KamiyoModParameters.PackageId, 900)
            });
            _util.Restart();
        }

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override int ChangeTargetSlot(BattleDiceCardModel card, BattleUnitModel target, int currentSlot,
            int targetSlot, bool teamkill)
        {
            return _util.AlwaysAimToTheSlowestDice(target);
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
            if (_util.UseSpecialBuffCard())
                SkinUtil.SakuraEffect(owner);
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
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