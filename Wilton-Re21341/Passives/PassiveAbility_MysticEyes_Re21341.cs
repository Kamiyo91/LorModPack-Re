using LOR_DiceSystem;

namespace KamiyoModPack.Wilton_Re21341.Passives
{
    public class PassiveAbility_MysticEyes_Re21341 : PassiveAbilityBase
    {
        private int _stacks;

        public override void OnWaveStart()
        {
            _stacks = 1;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash)
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, _stacks, owner);
            if (behavior.Detail == BehaviourDetail.Penetrate)
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, _stacks, owner);
        }

        public void ChangeStacks(int value)
        {
            _stacks = value;
        }
    }
}