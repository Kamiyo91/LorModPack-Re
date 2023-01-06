using BigDLL4221.Extensions;
using KamiyoModPack.Hayate_Re21341.Buffs;
using KamiyoModPack.Hayate_Re21341.Passives;

namespace KamiyoModPack.Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_Fingersnap_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChange;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            var buff = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_EntertainMe_Re21341);
            return buff != null && (buff.stack < -40 || buff.stack > 40);
        }

        public override void OnStartBattle()
        {
            var oldBuff = owner.GetActiveBuff<BattleUnitBuf_EntertainMe_Re21341>();
            var newBuffType = oldBuff.stack < -40;
            oldBuff.OnAddBuf(-999);
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

            owner.GetActivePassive<PassiveAbility_Hayate_Re21341>()?.Util
                .DeactivePermanentBuff(typeof(BattleUnitBuf_EntertainMe_Re21341));
            var buff = owner.GetActiveBuff<BattleUnitBuf_EntertainMe_Re21341>();
            if (buff != null) owner.bufListDetail.RemoveBuf(buff);
            if (!newBuffType)
                owner.bufListDetail.AddBuf(new BattleUnitBuf_ThisIsAllYouCanDo_Re21341());
            else
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Serious_Re21341());
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