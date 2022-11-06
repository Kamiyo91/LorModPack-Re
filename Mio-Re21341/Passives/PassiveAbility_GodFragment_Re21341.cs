using BigDLL4221.Passives;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragment_Re21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new MioUtil().MioPlayerUtil);
        }
    }
}
//public void ForcedEgo()
//{
//    _util.SetVipUnit();
//    _util.ChangeEgoAbDialog(new List<AbnormalityCardDialog>
//    {
//        new AbnormalityCardDialog
//        {
//            id = "Mio",
//            dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("MioEgoActive3_Re21341")).Value
//                .Desc
//        }
//    });
//    _util.ForcedEgo();
//    owner.personalEgoDetail.RemoveCard(new LorId(KamiyoModParameters.PackageId, 9));
//}