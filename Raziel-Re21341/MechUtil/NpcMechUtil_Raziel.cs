using System.Collections.Generic;
using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Enum;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;
using LOR_XML;

namespace KamiyoModPack.Raziel_Re21341.MechUtil
{
    public class NpcMechUtil_Raziel : NpcMechUtilBase
    {
        public NpcMechUtil_Raziel(NpcMechUtilBaseModel model) : base(model, KamiyoModParameters.PackageId)
        {
        }

        public override void ExtraMethodOnRoundEndTheLastIgnoreDead()
        {
            if (Model.Phase > 1 || !Model.Owner.IsDead()) return;
            UnitUtil.UnitReviveAndRecovery(Model.Owner, Model.Owner.MaxHp, false);
            UnitUtil.BattleAbDialog(Model.Owner.view.dialogUI,
                new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "RazielEnemy",
                        dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("RazielImmortal_Re21341")).Value.Desc
                    }
                }, AbColorType.Negative);
        }

        public override void ExtraMethodOnPhaseChangeRoundStart(MechPhaseOptions mechOptions)
        {
            Model.Owner.DieFake();
        }
    }
}