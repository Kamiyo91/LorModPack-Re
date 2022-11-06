using BigDLL4221.Passives;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_GodFragmentEnemy_Re21341 : PassiveAbility_NpcMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new MioUtil().MioNpcUtil);
        }
    }
}