using BigDLL4221.Extensions;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Dice
{
    public class DiceCardAbility_AlterEnergyDie_Re21341 : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            if (owner.GetActiveBuff<BattleUnitBuf_AlterEgoRelease_Re21341>() == null &&
                !owner.GetActivatedCustomEmotionCard(KamiyoModParameters.PackageId, 21345, out _)) return;
            behavior.card.target?.AddBuff<BattleUnitBuf_AlterEnergy_Re21341>(1);
        }
    }
}