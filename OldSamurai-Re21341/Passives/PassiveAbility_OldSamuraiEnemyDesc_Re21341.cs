using BLL_Re21341.Models.MechUtilModels;
using OldSamurai_Re21341.Buffs;
using OldSamurai_Re21341.MechUtil;
using Util_Re21341;

namespace OldSamurai_Re21341.Passives
{
    public class PassiveAbility_OldSamuraiEnemyDesc_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtil_OldSamurai _mechUtil;

        public override void OnWaveStart()
        {
            _mechUtil = new NpcMechUtil_OldSamurai(new NpcMechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_OldSamuraiEgoNpc_Re21341)
            });
            _mechUtil.Restart();
        }

        public override void OnBattleEnd()
        {
            _mechUtil.OnEndBattle();
        }

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override void OnDie()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_OldSamuraiEgoNpc_Re21341>()) return;
            UnitUtil.VipDeathNpc(owner);
        }

        public override void OnRoundStart()
        {
            if (_mechUtil.EgoCheck()) _mechUtil.EgoActive();
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _mechUtil.CheckPhase();
        }
    }
}