using KamiyoModPack.Hayate_Re21341.Buffs;
using KamiyoModPack.Hayate_Re21341.Passives;
using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_FingersnapBeforeLastPhase_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            var buff = owner.GetActiveBuff<BattleUnitBuf_EntertainMe_Re21341>();
            if (buff != null) buff.stack = 0;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player))
            {
                owner.GetActivePassive<PassiveAbility_HayateNpc_Re21341>()?.AddEmotionCards(unit);
                unit.Die();
            }
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