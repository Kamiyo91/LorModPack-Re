using System;
using UnityEngine;

namespace KamiyoModPack.Wilton_Re21341.Actions
{
    public class BehaviourAction_WiltonMassAttack_Re21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var farAreaeffectWilton = new GameObject().AddComponent<FarAreaEffect_WiltonMassAttack_Re21341>();
            farAreaeffectWilton.Init(self, Array.Empty<object>());
            return farAreaeffectWilton;
        }
    }
}