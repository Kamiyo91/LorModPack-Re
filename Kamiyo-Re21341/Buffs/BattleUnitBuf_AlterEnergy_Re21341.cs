using BigDLL4221.Buffs;
using BigDLL4221.Extensions;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_AlterEnergy_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
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
    }
}