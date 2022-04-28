using BLL_Re21341.Models;

namespace OldSamurai_Re21341.Buffs
{
    public class BattleUnitBuf_DeepBreathing_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_DeepBreathing_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "DeepBreathing_Re21341";
        protected override string keywordIconId => "DeepBreathing_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            owner.personalEgoDetail.RemoveCard(new LorId(KamiyoModParameters.PackageId, 1));
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}