using Sound;

namespace Roland_Re21341.Buffs
{
    public class BattleUnitBuf_BlackSilenceEgoMask_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_BlackSilenceEgoMask_Re21341()
        {
            stack = 0;
        }

        protected override string keywordId => "BlackSilenceEgo_Re21341";
        public override int paramInBufDesc => 0;
        protected override string keywordIconId => "BlackFrantic";
        public override bool isAssimilation => true;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }
    }
}