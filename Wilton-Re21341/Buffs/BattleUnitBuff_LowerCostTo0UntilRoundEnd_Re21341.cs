namespace KamiyoModPack.Wilton_Re21341.Buffs
{
    public class BattleUnitBuff_LowerCostTo0UntilRoundEnd_Re21341 : BattleUnitBuf
    {
        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return -99;
        }

        public override void OnRoundEndTheLast()
        {
            _owner.bufListDetail.RemoveBuf(this);
        }
    }
}