using System.Linq;
using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Mio_Re21341.Buffs;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Mio_Re21341.Passives
{
    public class PassiveAbility_MioMemory_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtilBase _util;

        public override void OnWaveStart()
        {
            _util = new NpcMechUtilBase(new NpcMechUtilBaseModel
            {
                Owner = owner,
                Hp = 0,
                SetHp = 179,
                MechHp = 271,
                Counter = 2,
                MaxCounter = 4,
                HasEgo = true,
                EgoActivated = true,
                RefreshUI = true,
                MassAttackStartCount = true,
                EgoType = typeof(BattleUnitBuf_GodAuraRelease_Re21341),
                LorIdEgoMassAttack = new LorId(ModParameters.PackageId, 900)
            });
            _util.EgoActive();
            if (BattleObjectManager.instance.GetList(owner.faction).FirstOrDefault(x => x != owner)?.hp > 161) return;
            owner.Die();
        }

        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
            if (owner.IsDead() &&
                BattleObjectManager.instance.GetList(owner.faction).FirstOrDefault(x => x != owner)?.hp > 161)
                UnitUtil.UnitReviveAndRecovery(owner, 67, false);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseCardResetCount(curCard);
        }
    }
}