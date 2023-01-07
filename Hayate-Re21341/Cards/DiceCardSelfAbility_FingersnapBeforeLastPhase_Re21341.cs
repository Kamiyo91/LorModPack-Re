using BigDLL4221.Extensions;
using KamiyoModPack.Hayate_Re21341.Buffs;

namespace KamiyoModPack.Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_FingersnapBeforeLastPhase_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            var buff = owner.GetActiveBuff<BattleUnitBuf_EntertainMe_Re21341>();
            if (buff != null) buff.stack = 0;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player)) unit.Die();
        }

        public override void OnApplyCard()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);
        }

        public override void OnReleaseCard()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}