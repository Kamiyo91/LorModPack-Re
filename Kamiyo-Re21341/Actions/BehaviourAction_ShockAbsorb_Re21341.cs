using System.Collections.Generic;

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
            return SetAttacksPlayer();
        }

        private static List<RencounterManager.MovingAction> SetAttacksPlayer()
        {
            return new List<RencounterManager.MovingAction>
            {
                CreateAttackAction(ActionDetail.Hit, 0.2f, "KamiyoHitEgo_Re21341", EffectTiming.PRE, EffectTiming.NONE,
                    EffectTiming.WITHOUT_DMGTEXT),
                CreateAttackAction(ActionDetail.Slash, 0.3f, "KamiyoSlashEgo_Re21341", EffectTiming.PRE,
                    EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT),
                CreateAttackAction(ActionDetail.Penetrate, 0.4f, "PierceKamiyoMask_Re21341", EffectTiming.PRE,
                    EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT),
                CreateAttackAction(ActionDetail.Hit, 0.5f, "KamiyoHitEgo_Re21341", EffectTiming.PRE, EffectTiming.PRE,
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
                new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.4f),
                new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.5f)
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