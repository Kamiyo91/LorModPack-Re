﻿using System.Collections.Generic;
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
    public class PassiveAbility_KurosawaButler_Re21341 : PassiveAbilityBase
    {
        private MechUtilBase _util;

        public override void OnWaveStart()
        {
            _util = new MechUtilBase(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                HasEgoAttack = true,
                EgoType = typeof(BattleUnitBuf_Vengeance_Re21341),
                EgoCardId = new LorId(ModParameters.PackageId, 47),
                EgoAttackCardId = new LorId(ModParameters.PackageId, 48),
                HasEgoAbDialog = true,
                EgoAbColorColor = AbColorType.Negative,
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "WiltonEnemy",
                        dialog = ModParameters.EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("WiltonEnemyEgoActive1_Re21341")).Value.Desc
                    }
                }
            });
            UnitUtil.CheckSkinProjection(owner);
        }

        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_Vengeance_Re21341>()) return stack + 1;
            return stack;
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

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }
    }
}