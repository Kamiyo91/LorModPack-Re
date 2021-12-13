﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_FingersnapBeforeLastPhase_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[On Use] Kill all enemies on the field and make them disappear at the end of the Scene.";

        public override void OnStartBattle()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player)) unit.Die();
        }

        public override void OnApplyCard() => owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);

        public override void OnReleaseCard() => owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
    }
}
