using BigDLL4221.CardAbility;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_WiltonFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_DLL4221
    {
        public override MapModel MapModel => KamiyoModParameters.WiltonMap;
        public override string SkinName => "Wilton_Re21341";

        public override void OnUseCard()
        {
            base.OnUseCard();
            foreach (var unit in BattleObjectManager.instance.GetAliveList(
                         UnitUtil.ReturnOtherSideFaction(owner.faction)))
            {
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Bleeding, 3, owner);
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 3, owner);
            }
        }
    }
}