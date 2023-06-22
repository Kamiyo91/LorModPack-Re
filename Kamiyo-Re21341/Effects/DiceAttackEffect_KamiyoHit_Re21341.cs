﻿using Battle.DiceAttackEffect;
using KamiyoModPack.BLL_Re21341.Models;
using UnityEngine;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Effects
{
    public class DiceAttackEffect_KamiyoHit_Re21341 : DiceAttackEffect
    {
        private const float Scale = 2.5f;
        private float _duration;

        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            base.Initialize(self, target, destroyTime);
            DiceEffectUtil.InitializeEffect<DiceAttackEffect_KamiyoHit_Re21341>(destroyTime, 0.54f, 0.32f, false, self,
                target, destroyTime, KamiyoModParameters.Path, gameObject, ref _self, ref _selfTransform,
                ref _targetTransform, ref _duration, ref spr, transform);
        }

        public override void SetScale(float scaleFactor)
        {
            base.SetScale(Scale);
        }

        protected override void Update()
        {
            base.Update();
            _duration -= Time.deltaTime;
            spr.color = new Color(1f, 1f, 1f, _duration * 2f);
        }
    }

    public class DiceAttackEffect_KamiyoHitEgo_Re21341 : DiceAttackEffect
    {
        private const float Scale = 2.5f;
        private float _duration;

        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            base.Initialize(self, target, destroyTime);
            DiceEffectUtil.InitializeEffect<DiceAttackEffect_KamiyoHitEgo_Re21341>(destroyTime, 0.54f, 0.32f, false,
                self,
                target, destroyTime, KamiyoModParameters.Path, gameObject, ref _self, ref _selfTransform,
                ref _targetTransform, ref _duration, ref spr, transform);
        }

        public override void SetScale(float scaleFactor)
        {
            base.SetScale(Scale);
        }

        protected override void Update()
        {
            base.Update();
            _duration -= Time.deltaTime;
            spr.color = new Color(1f, 1f, 1f, _duration * 2f);
        }
    }
}