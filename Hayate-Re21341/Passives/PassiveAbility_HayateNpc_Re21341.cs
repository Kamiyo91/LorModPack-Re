using BigDLL4221.Passives;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_HayateNpc_Re21341 : PassiveAbility_NpcMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new HayateUtil().HayateNpcUtil);
        }

        public void SetWiltonCaseOn()
        {
            Util.ExtraMethodCase();
        }
    }
}