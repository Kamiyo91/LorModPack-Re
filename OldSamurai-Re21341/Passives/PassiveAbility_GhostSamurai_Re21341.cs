using KamiyoModPack.OldSamurai_Re21341.Buffs;
using UtilLoader21341.Extensions;
using UtilLoader21341.Util;

namespace KamiyoModPack.OldSamurai_Re21341.Passives
{
    public class PassiveAbility_GhostSamurai_Re21341 : PassiveAbilityBase
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

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (owner.IsDead() && owner.faction == Faction.Enemy && BattleObjectManager.instance
                    .GetAliveList(Faction.Enemy)
                    .Exists(x => x.passiveDetail.HasPassive<PassiveAbility_OldSamuraiEnemyDesc_Re21341>()))
                owner.UnitReviveAndRecovery(25, false);
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