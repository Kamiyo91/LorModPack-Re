using Kamiyo_Re21341.Buffs;
using Mio_Re21341.Passives;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_KurosawaEmblem_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.passiveDetail.HasPassive<PassiveAbility_GodFragment_Re21341>())
                owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_KurosawaEmblem_Re21341());
            else owner.passiveDetail.DestroyPassive(this);
        }
    }
}