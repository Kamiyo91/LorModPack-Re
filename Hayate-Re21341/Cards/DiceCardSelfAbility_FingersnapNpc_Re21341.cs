﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_FingersnapNpc_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            card.target.Die(owner);
        }

        public override bool IsTargetChangable(BattleUnitModel attacker) => false;

        public override void OnApplyCard() => owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);

        public override void OnReleaseCard() => owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
    }
}
