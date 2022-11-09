using BigDLL4221.Buffs;
using KamiyoModPack.Wilton_Re21341.Passives;
using Sound;
using UnityEngine;

namespace KamiyoModPack.Wilton_Re21341.Buffs
{
    public class BattleUnitBuf_Vengeance_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        public BattleUnitBuf_Vengeance_Re21341() : base(infinite: true, lastOneScene: false)
        {
        }

        public override bool isAssimilation => true;
        protected override string keywordId => "Vengeance_Re21341";
        protected override string keywordIconId => "RedHood_Rage";
        public override int MaxStack => 50;
        public override int MinStack => 1;
        public override int AdderStackEachScene => -1;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            var effect = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "8_B/FX_IllusionCard_8_B_Punising",
                1f, _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/SmallBird_StrongAtk");
            foreach (var particle in effect.gameObject.GetComponentsInChildren<ParticleSystem>())
                if (particle.gameObject.name.Contains("Bird") || particle.gameObject.name.Contains("Main"))
                    particle.gameObject.SetActive(false);
            var passive = owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_MysticEyes_Re21341) as
                PassiveAbility_MysticEyes_Re21341;
            passive?.ChangeStacks(2);
            passive?.SetBuff(this);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
            if (stack > 24)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = 1
                    });
        }

        public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
        {
            if (target == _owner) return 0;
            if (cardBuf.bufType == KeywordBuf.Vulnerable || cardBuf.bufType == KeywordBuf.Bleeding) OnAddBuf(1);
            return 1;
        }
    }
}