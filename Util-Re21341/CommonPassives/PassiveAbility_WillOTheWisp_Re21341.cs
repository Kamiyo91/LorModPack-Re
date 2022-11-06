using System.Linq;
using BigDLL4221.Buffs;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_WillOTheWisp_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.Book.BookId != new LorId(KamiyoModParameters.PackageId, 9)) return;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_WolfBlueAura_DLL4221());
        }

        public override void OnRoundEnd()
        {
            if (BattleObjectManager.instance.GetAliveList(owner.faction)
                    .Count(x => x.passiveDetail.HasPassive<PassiveAbility_WillOTheWisp_Re21341>()) <= 2) return;
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, owner);
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, owner);
        }
    }
}