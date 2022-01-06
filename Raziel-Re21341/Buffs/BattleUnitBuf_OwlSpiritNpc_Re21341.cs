using Sound;

namespace Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_OwlSpiritNpc_Re21341 : BattleUnitBuf
    {
        private static readonly int Power = RandomUtil.Range(0, 2);

        public BattleUnitBuf_OwlSpiritNpc_Re21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "Kaioken_Re21341";
        protected override string keywordIconId => "Kaioken_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect("8_B/FX_IllusionCard_8_B_Punising",
                1f, _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/SmallBird_StrongAtk");
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            var pow = Power;
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                min = pow,
                max = pow
            });
        }
    }
}