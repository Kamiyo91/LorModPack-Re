using System;
using UnityEngine;

namespace KamiyoModPack.Mio_Re21341.Actions
{
    public class BehaviourAction_MioAbyssalDiamond_Re21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var farAreaeffectMio = new GameObject().AddComponent<FarAreaEffect_MioAbyssalDiamond_Re21341>();
            farAreaeffectMio.Init(self, Array.Empty<object>());
            return farAreaeffectMio;
        }
    }
}