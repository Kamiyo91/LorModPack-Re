using Kamiyo_Re21341.Buffs;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_PossesorOfMioLight_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.passiveDetail.HasPassive<PassiveAbility_AlterEgoPlayer_Re21341>())
                owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_PossessorOfMioLight_Re21341());
            else owner.passiveDetail.DestroyPassive(this);
        }
    }
}