using BigDLL4221.Passives;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Wilton_Re21341.Passives
{
    public class PassiveAbility_KurosawaButlerEnemy_Re21341 : PassiveAbility_NpcMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new WiltonUtil().WiltonNpcUtil);
        }
    }
}