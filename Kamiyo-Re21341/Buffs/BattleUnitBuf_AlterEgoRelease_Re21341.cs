using BigDLL4221.Buffs;
using BigDLL4221.Extensions;
using Sound;
using UnityEngine;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_AlterEgoRelease_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        public BattleUnitBuf_AlterEgoRelease_Re21341() : base(infinite: true, lastOneScene: false)
        {
        }

        public override bool isAssimilation => true;
        protected override string keywordId => "AlterEgoMask_Re21341";
        protected override string keywordIconId => "AlterEgoMask_Re21341";
        public override int MaxStack => 30;
        public override int MinStack => 1;
        public override int AdderStackEachScene => -3;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            PlayChangingEffect(owner);
        }

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            if (stack > 14)
                behavior.card?.target?.AddBuff<BattleUnitBuf_AlterEnergy_Re21341>(1);
        }

        public override void OnRoundStart()
        {
            if (stack > 4) _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 1, _owner);
            _owner.TakeDamage(stack > 24 ? 9 : stack > 14 ? 7 : stack > 4 ? 5 : 3);
        }

        private void PlayChangingEffect(BattleUnitModel owner)
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            var aura = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "2_Y/FX_IllusionCard_2_Y_Charge", 1f, _owner.view, _owner.view);
            foreach (var particle in aura.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (particle.gameObject.name.Contains("Burn"))
                    particle.gameObject.AddComponent<AuraColor>();
                if (!particle.gameObject.name.Equals("Main") && !particle.gameObject.name.Contains("Charge") &&
                    !particle.gameObject.name.Contains("Scaner_holo_distortion")) continue;
                particle.gameObject.SetActive(false);
            }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            OnAddBuf(1);
            behavior.card.target?.AddBuff<BattleUnitBuf_AlterEnergy_Re21341>(1);
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (_owner.bufListDetail.HasBuf<BattleUnitBuf_NearDeath_Re21341>() ||
                _owner.bufListDetail.HasBuf<BattleUnitBuf_NearDeathNpc_Re21341>()) return;
            OnAddBuf(-1);
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return card.GetOriginCost() < 3 ? -1 : base.GetCardCostAdder(card);
        }
    }
}