using BLL_Re21341.Models;

namespace Mio_Re21341.Buffs
{
    public class BattleUnitBuf_KurosawaEmblem_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_KurosawaEmblem_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "KurosawaEmblem_Re21341";
        protected override string keywordIconId => "KurosawaEmblem_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().FindAll(x =>
                         x.GetID() == new LorId(ModParameters.PackageId, 14) ||
                         x.GetID() == new LorId(ModParameters.PackageId, 12)))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
        }
    }
}