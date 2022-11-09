using BigDLL4221.Extensions;
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
            var buff = owner.GetActiveBuff<BattleUnitBuf_Vengeance_Re21341>();
            if (behavior.Detail == BehaviourDetail.Slash)
            {
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, _stacks, owner);
                buff?.OnAddBuf(1);
            }

            if (behavior.Detail != BehaviourDetail.Penetrate) return;
            behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, _stacks, owner);
            buff?.OnAddBuf(1);
        }

        public void ChangeStacks(int value)
        {
            _stacks = value;
        }

        public void SetBuff(BattleUnitBuf_Vengeance_Re21341 buf)
        {
            _egoBuff = buf;
        }
    }
}