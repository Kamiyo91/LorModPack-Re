using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sound;

namespace Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_OwlSpiritNpc_Re21341 : BattleUnitBuf
    {
        private static int Power = RandomUtil.Range(0, 2);
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect("8_B/FX_IllusionCard_8_B_Punising", 1f, _owner.view, _owner.view);
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
