using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.CreatureEffect;
using Sound;
using UnityEngine;

namespace Wilton_Re21341.Buffs
{
    public class BattleUnitBuf_Vengeance_Re21341 : BattleUnitBuf
    {
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect("8_B/FX_IllusionCard_8_B_Punising", 1f, owner.view, owner.view);
            SoundEffectPlayer.PlaySound("Creature/SmallBird_StrongAtk");
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
