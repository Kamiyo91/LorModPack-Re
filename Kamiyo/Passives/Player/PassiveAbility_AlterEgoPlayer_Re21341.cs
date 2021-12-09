﻿using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Kamiyo_Re21341.Buffs;
using LOR_XML;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Kamiyo_Re21341.Passives.Player
{
    public class PassiveAbility_AlterEgoPlayer_Re21341 : PassiveAbilityBase
    {
        private MechUtilBase _util;
        public override void OnWaveStart()
        {
            _util = new MechUtilBase(new MechUtilBaseModel { Owner = owner, 
                Hp = 25, 
                SetHp = 25, 
                Survive = true, 
                HasEgo = true, 
                RefreshUI = true,
                SkinName = "KamiyoMask-Re21341", 
                EgoType = typeof(BattleUnitBuf_AlterEgoRelease_Re21341), 
                EgoCardId = new LorId(ModParameters.PackageId, 1) });
            UnitUtil.TestingUnitValues();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.SurviveCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
            UnitUtil.BattleAbDialog(owner.view.dialogUI, new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog {id = "Kamiyo", dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive1_Re21341")).Value},
                new AbnormalityCardDialog {id = "Kamiyo", dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive2_Re21341")).Value},
                new AbnormalityCardDialog {id = "Kamiyo", dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive3_Re21341")).Value}
            },false);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }
    }
}
