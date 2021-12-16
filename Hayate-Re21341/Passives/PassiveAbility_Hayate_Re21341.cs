using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using BLL_Re21341.Models.MechUtilModels;
using Hayate_Re21341.Buffs;
using Hayate_Re21341.MechUtil;
using LOR_XML;
using Util_Re21341;

namespace Hayate_Re21341.Passives
{
    public class PassiveAbility_Hayate_Re21341 : PassiveAbilityBase
    {
        private MechUtil_Hayate _util;

        public override void OnWaveStart()
        {
            _util = new MechUtil_Hayate(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_TrueGodAuraRelease_Re21341),
                EgoCardId = new LorId(ModParameters.PackageId, 28),
                HasEgoAttack = true,
                EgoAttackCardId = new LorId(ModParameters.PackageId, 29),
                HasEgoAbDialog = true,
                EgoAttackCardExpire = true,
                EgoAbColorColor = AbColorType.Positive,
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Hayate",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("HayateEgoActive1_Re21341"))
                            .Value.Desc
                    },
                    new AbnormalityCardDialog
                    {
                        id = "Hayate",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("HayateEgoActive2_Re21341"))
                            .Value.Desc
                    }
                }
            });
            UnitUtil.CheckSkinProjection(owner);
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