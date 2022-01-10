using System;
using UnityEngine;

namespace Omori_Re21341.DiceEffects
{
    public class BehaviourAction_OmoriHackAway_Re21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var omoriHackAway = new GameObject().AddComponent<FarAreaEffect_OmoriHackAway_Re21341>();
            omoriHackAway.Init(self, Array.Empty<object>());
            return omoriHackAway;
        }
    }
}