using BigDLL4221.Buffs;
using BigDLL4221.Extensions;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_AlterEnergy_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        //private CreatureEffect Aura;
        public BattleUnitBuf_AlterEnergy_Re21341() : base(infinite: false, lastOneScene: false)
        {
        }

        protected override string keywordId => "AlterEnergy_Re21341";
        protected override string keywordIconId => "AlterEnergy_Re21341";
        public override int AdderStackEachScene => -2;
        public override int MaxStack => 10;
        public override bool DestroyedAt0Stack => true;

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (atkDice.owner.GetActiveBuff<BattleUnitBuf_AlterEgoRelease_Re21341>() != null ||
                atkDice.owner.GetActivatedCustomEmotionCard(KamiyoModParameters.PackageId, 21345, out _))
                _owner.TakeDamage(stack, DamageType.Buf);
        }

        //public override void Init(BattleUnitModel owner)
        //{
        //    base.Init(owner);
        //    InitAura();
        //}

        //private void InitAura()
        //{
        //    Aura = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
        //        "5_T/FX_IllusionCard_5_T_Happiness", 1f, _owner.view, _owner.view);
        //    foreach (var particle in Aura.gameObject.GetComponentsInChildren<ParticleSystem>())
        //    {
        //        if (!particle.gameObject.name.Equals("Force"))
        //        {
        //            particle.gameObject.SetActive(false);
        //            continue;
        //        }

        //        var main = particle.main;
        //        main.startColor = new Color(1, 1, 1, 0);
        //        main.startLifetimeMultiplier = 0.5f;
        //        main.startSizeMultiplier = 0.1f;
        //    }
        //}
    }
}