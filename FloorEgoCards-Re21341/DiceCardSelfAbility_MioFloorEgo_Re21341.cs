using BigDLL4221.CardAbility;
using BigDLL4221.Models;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Mio_Re21341.Buffs;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_MioFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_DLL4221
    {
        public override MapModel MapModel => KamiyoModParameters.MioMap;
        public override string SkinName => "MioRedEye_Re21341";

        public override void OnUseCard()
        {
            base.OnUseCard();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 3, owner);
            owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_SakuraPetalOneScene_Re21341());
        }
    }
}