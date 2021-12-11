using BLL_Re21341.Models;
using Util_Re21341;

namespace OldSamurai_Re21341.Buffs
{
    public class BattleUnitBuf_OldSamuraiEgoNpc_Re21341 : BattleUnitBuf
    {
        public override bool isAssimilation => true;
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            for (var i = 1; i < 4; i++)

                UnitUtil.AddNewUnitEnemySide(new UnitModel
                {
                    Id = 2,
                    Pos = i,
                    LockedEmotion = true
                });
            UnitUtil.RefreshCombatUI();
        }
    }
}
