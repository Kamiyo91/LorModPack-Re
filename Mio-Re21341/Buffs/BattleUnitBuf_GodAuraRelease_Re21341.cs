using BigDLL4221.Buffs;
using Sound;
using UnityEngine;

namespace KamiyoModPack.Mio_Re21341.Buffs
{
    public class BattleUnitBuf_GodAuraRelease_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        public BattleUnitBuf_GodAuraRelease_Re21341() : base(infinite: true, lastOneScene: false)
        {
        }

        public override bool isAssimilation => true;
        protected override string keywordId => "GodAura_Re21341";
        protected override string keywordIconId => "Light_Re21341";
        public override int MaxStack => 30;
        public override int MinStack => 1;
        public override int AdderStackEachScene => -1;

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
            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            var effect = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "5_T/FX_IllusionCard_5_T_Happiness", 1f, _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
            foreach (var particle in effect.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (!particle.gameObject.name.Contains("Force"))
                {
                    particle.gameObject.SetActive(false);
                    continue;
                }

                var main = particle.main;
                main.startColor = particle.gameObject.name.Equals("Force_burn")
                    ? new Color(1, 0, 0, 1)
                    : new Color(1, 1, 0.702f, 1);
            }
        }


        public override void OnRoundEnd()
        {
            RecoverHpAndStagger();
            base.OnRoundEnd();
        }

        private void RecoverHpAndStagger()
        {
            _owner.RecoverHP(3);
            _owner.breakDetail.RecoverBreak(3);
            if (stack > 2) _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, stack / 3, _owner);
            if (stack > 9) _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, stack / 10, _owner);
            if (stack > 14) _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, stack / 15, _owner);
        }
    }
}