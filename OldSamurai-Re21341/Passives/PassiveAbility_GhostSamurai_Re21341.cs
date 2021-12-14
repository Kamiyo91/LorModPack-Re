using System.Linq;
using OldSamurai_Re21341.Buffs;
using Util_Re21341;
using Util_Re21341.CommonBuffs;

namespace OldSamurai_Re21341.Passives
{
    public class PassiveAbility_GhostSamurai_Re21341 : PassiveAbilityBase
    {
        private void AddGhostUnitBuffs()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_KeterFinal_LibrarianAura());
            if (owner.faction == Faction.Player) 
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Uncontrollable_Re21341());
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
                ? typeof(BattleUnitBuf_Uncontrollable_Re21341)
                : typeof(BattleUnitBuf_GhostSamuraiEnemy_Re21341));
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (owner.IsDead() && owner.faction == Faction.Enemy && BattleObjectManager.instance.GetAliveList(Faction.Enemy).Exists(x => x.bufListDetail.HasBuf<BattleUnitBuf_OldSamuraiEgoNpc_Re21341>())) UnitUtil.UnitReviveAndRecovery(owner, 25,false);
        }

        public override void OnWaveStart()
        {
            AddGhostUnitBuffs();
        }

        public override void OnDie()
        {
            CleanGhostUnitBuffs();
        }
    }
}
