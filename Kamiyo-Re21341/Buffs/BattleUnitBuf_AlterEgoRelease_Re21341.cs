using BigDLL4221.Buffs;
using LOR_DiceSystem;
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
        public override int MaxStack => 10;
        public override int MinStack => 1;
        public override int AdderStackEachScene => -2;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = stack > 7 ? 2 : 1
                });
            if (stack > 4 && behavior.Detail == BehaviourDetail.Evasion)
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

        public override void OnRoundStart()
        {
            _owner.TakeDamage(stack > 7 ? 8 : stack > 4 ? 5 : 3);
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
        }
    }
}