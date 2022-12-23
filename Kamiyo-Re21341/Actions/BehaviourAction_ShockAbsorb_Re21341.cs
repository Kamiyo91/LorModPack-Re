using System.Collections.Generic;
using BigDLL4221.Extensions;
using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Actions
{
    public class BehaviourAction_ShockAbsorb_Re21341 : BehaviourActionBase
    {
        public override bool IsMovable()
        {
            return false;
        }

        public override bool IsOpponentMovable()
        {
            return false;
        }

        public override List<RencounterManager.MovingAction> GetMovingAction(
            ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            if (self.result != Result.Win || opponent.behaviourResultData.IsFarAtk())
                return base.GetMovingAction(ref self, ref opponent);
            _self = self.view.model;
            if (opponent.infoList.Count > 0)
                opponent.infoList.Clear();
            opponent.infoList = SetDamageEnemy();
            return SetAttacksPlayer(_self);
        }

        private static List<RencounterManager.MovingAction> SetAttacksPlayer(BattleUnitModel self)
        {
            var hasEgoBuff = self.GetActiveBuff<BattleUnitBuf_AlterEgoRelease_Re21341>() != null;
            var isForgotten = !hasEgoBuff && self.Book.BookId == new LorId("VortexTowerModSa21341.Mod", 10000005);
            return new List<RencounterManager.MovingAction>
            {
                CreateAttackAction(ActionDetail.Hit, 0.2f,
                    hasEgoBuff ? "KamiyoHitEgo_Re21341" :
                    isForgotten ? "KamiyoHitForgotten_Sa21341" : "KamiyoHit_Re21341", EffectTiming.PRE,
                    EffectTiming.NONE,
                    EffectTiming.WITHOUT_DMGTEXT),
                CreateAttackAction(ActionDetail.Slash, 0.3f,
                    hasEgoBuff ? "KamiyoSlashEgo_Re21341" :
                    isForgotten ? "KamiyoSlashForgotten_Sa21341" : "KamiyoSlash_Re21341", EffectTiming.PRE,
                    EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT),
                CreateAttackAction(ActionDetail.Penetrate, 0.65f,
                    hasEgoBuff ? "PierceKamiyoMask_Re21341" :
                    isForgotten ? "PierceKamiyoForgotten_Sa21341" : "PierceKamiyo_Re21341", EffectTiming.PRE,
                    EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT),
                CreateAttackAction(ActionDetail.Hit, 0.5f,
                    hasEgoBuff ? "KamiyoHitEgo_Re21341" :
                    isForgotten ? "KamiyoHitForgotten_Sa21341" : "KamiyoHit_Re21341", EffectTiming.PRE,
                    EffectTiming.PRE,
                    EffectTiming.PRE)
            };
            ;
        }

        private static List<RencounterManager.MovingAction> SetDamageEnemy()
        {
            return new List<RencounterManager.MovingAction>
            {
                new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.2f),
                new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.3f),
                new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.65f),
                new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, 1f, true, 0.5f)
                    { knockbackPower = 10f }
            };
        }

        private static RencounterManager.MovingAction CreateAttackAction(ActionDetail action, float delay,
            string effectRes, EffectTiming attackTiming, EffectTiming recoverTiming, EffectTiming damageDisplayTiming)
        {
            var movingAction = new RencounterManager.MovingAction(action, CharMoveState.Stop, 0f, true, delay);
            movingAction.customEffectRes = effectRes;
            movingAction.SetEffectTiming(attackTiming, recoverTiming, damageDisplayTiming);
            return movingAction;
        }
    }
}