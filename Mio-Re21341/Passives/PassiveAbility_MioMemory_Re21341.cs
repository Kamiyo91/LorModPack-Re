using BigDLL4221.Passives;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_MioMemory_Re21341 : PassiveAbility_UnitSummonedLinkedToMainChar_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetParameters(new KamiyoUtil().MioMemoryUtil);
            SetCounter(2);
        }
    }
}