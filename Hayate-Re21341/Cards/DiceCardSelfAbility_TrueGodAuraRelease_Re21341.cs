using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_TrueGodAuraRelease_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[Single Use]\nCan be used at Emotion Level 4 or above\n[On Use] Unleash The True Power of a God,recover full Stagger Resist and full Light next Scene.";

        public override bool OnChooseCard(BattleUnitModel owner) => owner.emotionDetail.EmotionLevel >= 4 && !owner.bufListDetail.HasAssimilation();
    }
}
