using Sound;
using UnityEngine;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_AlterEgoRelease_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_AlterEgoRelease_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        public override bool isAssimilation => true;
        protected override string keywordId => "AlterEgoMask_Re21341";
        protected override string keywordIconId => "AlterEgoMask_Re21341";

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

        public override void OnRoundStart()
        {
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 1, _owner);
            _owner.TakeDamage(3);
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
            behavior.card.target?.AddBuff<BattleUnitBuf_AlterEnergy_Re21341>(1,maxStack:10);
        }
    }
}