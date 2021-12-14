using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Roland_Re21341.DiceEffects
{
    public class BehaviourAction_BlackSilenceCustomEgoAreaStrong_Re21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var farAreaeffectBlackSilence4ThAreaStrong =
                new GameObject().AddComponent<FarAreaEffect_BlackSilenceCustomEgoAreaStrong_Re21341>();
            farAreaeffectBlackSilence4ThAreaStrong.Init(self, Array.Empty<object>());
            return farAreaeffectBlackSilence4ThAreaStrong;
        }
    }
}
