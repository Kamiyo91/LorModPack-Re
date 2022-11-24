using BigDLL4221.CardAbility;
using BigDLL4221.Models;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_KamiyoFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_DLL4221
    {
        public override MapModel MapModel => KamiyoModParameters.KamiyoMap2;
        public override string SkinName => "KamiyoMask_Re21341";
    }
}