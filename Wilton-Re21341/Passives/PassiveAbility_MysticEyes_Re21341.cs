using KamiyoModPack.Wilton_Re21341.Buffs;
using LOR_DiceSystem;

namespace KamiyoModPack.Wilton_Re21341.Passives
{
    public class PassiveAbility_MysticEyes_Re21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_Vengeance_Re21341 _egoBuff;
        private int _stacks;

        public override void OnWaveStart()
        {
            _egoBuff = null;
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

        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            if (_egoBuff == null) base.OnGiveKeywordBufByCard(buf, stack, target);
            if (buf.bufType == KeywordBuf.Vulnerable || buf.bufType == KeywordBuf.Bleeding) _egoBuff?.OnAddBuf(1);
            return base.OnGiveKeywordBufByCard(buf, stack, target);
        }

        public void SetBuff(BattleUnitBuf_Vengeance_Re21341 buf)
        {
            _egoBuff = buf;
        }
    }
}