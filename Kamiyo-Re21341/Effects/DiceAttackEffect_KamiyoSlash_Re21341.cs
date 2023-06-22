using Battle.DiceAttackEffect;
using KamiyoModPack.BLL_Re21341.Models;
using UnityEngine;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Effects
{
    public class DiceAttackEffect_KamiyoSlash_Re21341 : DiceAttackEffect
    {
        private const float Scale = 2.5f;
        private float _duration;

        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            base.Initialize(self, target, destroyTime);
            _duration = _destroyTime;
            DiceEffectUtil.InitializeEffect(KamiyoModParameters.Path, 0.55f, 0.15f, true, this, self, target);
        }

        public override void SetScale(float scaleFactor)
        {
            base.SetScale(DiceEffectUtil.CalculateScale(false, scaleFactor, Scale));
        }

        protected override void Update()
        {
            base.Update();
            _duration -= Time.deltaTime;
            spr.color = new Color(1f, 1f, 1f, _duration * 2f);
        }
    }

    public class DiceAttackEffect_KamiyoSlashEgo_Re21341 : DiceAttackEffect
    {
        private const float Scale = 2.5f;
        private float _duration;

        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            base.Initialize(self, target, destroyTime);
            _duration = _destroyTime;
            DiceEffectUtil.InitializeEffect(KamiyoModParameters.Path, 0.55f, 0.15f, true, this, self, target);
        }

        public override void SetScale(float scaleFactor)
        {
            base.SetScale(DiceEffectUtil.CalculateScale(false, scaleFactor, Scale));
        }

        protected override void Update()
        {
            base.Update();
            _duration -= Time.deltaTime;
            spr.color = new Color(1f, 1f, 1f, _duration * 2f);
        }
    }
}