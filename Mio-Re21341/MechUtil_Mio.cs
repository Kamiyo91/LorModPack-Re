using System.Collections.Generic;
using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Extensions;
using BigDLL4221.Models;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Util_Re21341.CommonBuffs;
using LOR_XML;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class MechUtil_Mio : MechUtilBase
    {
        public MechUtil_Mio(MechUtilBaseModel model) : base(model)
        {
        }

        public override bool EgoActive()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id != 2 ||
                !Model.Owner.IsSupportCharCheck()) return base.EgoActive();
            Model.Owner.bufListDetail.AddBuf(new BattleUnitBuf_Vip_Re21341());
            ChangeEgoAbDialog(new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "Mio",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("MioEgoActive3_Re21341")).Value
                        .Desc
                }
            });

            return base.EgoActive();
        }
    }
}