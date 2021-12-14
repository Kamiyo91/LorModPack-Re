using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hayate_Re21341.Buffs;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_Fingersnap_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChange;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel >= 5 && owner.bufListDetail.GetActivatedBufList()
                .Find(x => x is BattleUnitBuf_EntertainMe_Re21341).stack >= 40;
        }

        public override void OnStartBattle()
        {
            if (_motionChange)
            {
                _motionChange = false;
                owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            }
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Enemy))
                if (unit.MaxHp < 250)
                {
                    unit.Die(owner);
                }
                else
                {
                    unit.TakeDamage(250, DamageType.ETC);
                    unit.breakDetail.TakeBreakDamage(250, DamageType.ETC);
                }
        }

        public override void OnApplyCard()
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem != owner.UnitData.unitData.CustomBookItem) return;
            _motionChange = true;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);
        }

        public override void OnReleaseCard()
        {
            _motionChange = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}
