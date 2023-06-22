using KamiyoModPack.Hayate_Re21341.Buffs;
using LOR_DiceSystem;
using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_Ultima_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 3;
        private int _atkLand;
        public override string[] Keywords => new[] { "UltimaKeyword_Re21341" };

        public override void OnUseCard()
        {
            _atkLand = 0;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk) _atkLand++;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check || card.target == null) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(card.target.faction))
            {
                var ultimaBuff = unit.GetActiveBuff<BattleUnitBuf_Ultima_Re21341>();
                if (ultimaBuff != null) unit.bufListDetail.RemoveBuf(ultimaBuff);
            }

            card.target.bufListDetail.AddBuf(new BattleUnitBuf_Ultima_Re21341());
        }
    }
}