using UnityEngine;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_AlterEnergy_Re21341 : BattleUnitBuf
    {
        private GameObject _aura;
        protected override string keywordId => "AlterEnergy_Re21341";
        protected override string keywordIconId => "AlterEnergy_Re21341";
        public int MaxStack => 10;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            InitAura();
        }

        public override void OnRoundEndTheLast()
        {
            _owner.TakeDamage(stack, DamageType.Buf);
            RemoveBuff();
        }

        private void InitAura()
        {
            var effect = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "5_T/FX_IllusionCard_5_T_Happiness", 1f, _owner.view, _owner.view);
            foreach (var particle in effect.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (!particle.gameObject.name.Equals("Force"))
                {
                    particle.gameObject.SetActive(false);
                    continue;
                }

                var main = particle.main;
                main.startColor = new Color(1, 1, 1, 0);
                main.startLifetimeMultiplier = 0.5f;
                main.startSizeMultiplier = 0.1f;
            }

            _aura = effect.gameObject;
        }

        private void RemoveBuff()
        {
            if (_aura != null) Object.Destroy(_aura);
            _owner.bufListDetail.RemoveBuf(this);
        }
    }
}