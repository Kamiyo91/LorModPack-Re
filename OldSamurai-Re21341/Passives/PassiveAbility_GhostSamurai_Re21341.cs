using BigDLL4221.Buffs;
using BigDLL4221.Passives;
using BigDLL4221.Utils;
using KamiyoModPack.OldSamurai_Re21341.Buffs;

namespace KamiyoModPack.OldSamurai_Re21341.Passives
{
    public class PassiveAbility_GhostSamurai_Re21341 : PassiveAbility_SupportChar_DLL4221
    {
        private void AddGhostUnitBuffs()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_KeterFinal_LibrarianAura());
            if (owner.faction == Faction.Player)
                owner.bufListDetail.AddBuf(new BattleUnitBuf_Uncontrollable_DLL4221());
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
                ? typeof(BattleUnitBuf_Uncontrollable_DLL4221)
                : typeof(BattleUnitBuf_GhostSamuraiEnemy_Re21341));
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (owner.IsDead() && owner.faction == Faction.Enemy && BattleObjectManager.instance
                    .GetAliveList(Faction.Enemy)
                    .Exists(x => x.passiveDetail.HasPassive<PassiveAbility_OldSamuraiEnemyDesc_Re21341>()))
                UnitUtil.UnitReviveAndRecovery(owner, 25, false);
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