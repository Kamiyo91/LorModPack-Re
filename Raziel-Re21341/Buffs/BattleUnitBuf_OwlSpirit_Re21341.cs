﻿using BigDLL4221.Buffs;
using Sound;
using UnityEngine;
using Random = System.Random;

namespace KamiyoModPack.Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_OwlSpirit_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        private GameObject _aura;
        private int _damageCount;
        private Random _random;

        public BattleUnitBuf_OwlSpirit_Re21341() : base(infinite: true, lastOneScene: false)
        {
        }

        public override bool isAssimilation => true;
        protected override string keywordId => "Kaioken_Re21341";
        protected override string keywordIconId => "Kaioken_Re21341";
        public override int MaxStack => 5;
        public override int MinStack => 1;

        public override bool IsInvincibleBp(BattleUnitModel attacker)
        {
            return true;
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _random = new Random();
            _damageCount = 0;
            var effect =
                SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                    "8_B/FX_IllusionCard_8_B_Punising", 1f, _owner.view, _owner.view);
            _aura = effect != null ? effect.gameObject : null;
            SoundEffectPlayer.PlaySound("Creature/SmallBird_StrongAtk");
            if (_aura == null) return;
            foreach (var particle in _aura.gameObject.GetComponentsInChildren<ParticleSystem>())
                if (particle.gameObject.name.Contains("Bird"))
                    particle.gameObject.SetActive(false);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            var pow = _random.Next(0, stack);
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                max = pow
            });
        }

        public override void BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _damageCount += dmg;
        }

        public override void OnRoundStartAfter()
        {
            _owner.TakeDamage(15);
            if (_damageCount > 25 && stack < 2) OnAddBuf(1);
            if (_damageCount > 50 && stack < 3) OnAddBuf(1);
            if (_damageCount > 75 && stack < 4) OnAddBuf(1);
            if (_damageCount > 100 && stack < 5) OnAddBuf(1);
        }
    }
}