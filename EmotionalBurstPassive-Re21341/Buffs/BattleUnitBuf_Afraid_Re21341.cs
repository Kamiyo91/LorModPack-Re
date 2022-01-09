using BLL_Re21341.Models.Enum;
using LOR_DiceSystem;

namespace EmotionalBurstPassive_Re21341.Buffs
{
    public class BattleUnitBuf_Afraid_Re21341 : BattleUnitBuf
    {
        protected override string keywordId => "Afraid_Re21341";
        protected override string keywordIconId => "Afraid_Re21341";

        public override int GetDamageIncreaseRate()
        {
            return 50;
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            EmotionalBurstUtil.RemoveAllEmotionalPassives(owner, EmotionBufEnum.Afraid);
        }

        public override void OnRoundEnd()
        {
            stack--;
            if (stack < 1)
            {
                Destroy();
                return;
            }

            _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, stack, _owner);
        }

        public override bool IsCardChoosable(BattleDiceCardModel card)
        {
            if (card.XmlData.Spec.Ranged == CardRange.FarArea ||
                card.XmlData.Spec.Ranged == CardRange.FarAreaEach || card.GetOriginCost() > 3 ||
                card.XmlData.IsEgo()) return false;
            return base.IsCardChoosable(card);
        }
    }
}