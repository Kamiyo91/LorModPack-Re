using KamiyoModPack.OldSamurai_Re21341.Buffs;
using UtilLoader21341.Extensions;

namespace KamiyoModPack.OldSamurai_Re21341.Passives
{
    public class PassiveAbility_GhostSamuraiEmotion_Re21341 : PassiveAbilityBase
    {
        private void AddGhostUnitBuffs()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_KeterFinal_LibrarianAura());
            if (owner.faction == Faction.Player)
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Uncontrollable_DLL21341());
            else
                owner.bufListDetail.AddBuf(new BattleUnitBuf_GhostSamuraiEnemy_Re21341());
        }

        private void CleanGhostUnitBuffs()
        {
            if (owner.bufListDetail.GetActivatedBufList()
                    .Find(x => x is BattleUnitBuf_KeterFinal_LibrarianAura) is BattleUnitBuf_KeterFinal_LibrarianAura
                bufAura)
                bufAura.Destroy();

            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_KeterFinal_LibrarianAura));
            owner.bufListDetail.RemoveBufAll(owner.faction == Faction.Player
                ? typeof(BattleUnitBuf_Uncontrollable_DLL21341)
                : typeof(BattleUnitBuf_GhostSamuraiEnemy_Re21341));
        }

        public override void OnWaveStart()
        {
            AddGhostUnitBuffs();
            base.OnWaveStart();
        }

        public override void OnDie()
        {
            CleanGhostUnitBuffs();
        }
    }
}