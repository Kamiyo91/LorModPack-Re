using BigDLL4221.Passives;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_Hayate_Re21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new HayateUtil().HayatePlayerUtil);
        }
    }
}