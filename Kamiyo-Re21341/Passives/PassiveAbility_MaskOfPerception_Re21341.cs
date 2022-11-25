using BigDLL4221.Extensions;
using BigDLL4221.Utils;
using KamiyoModPack.Kamiyo_Re21341.Buffs;
using LOR_DiceSystem;

namespace KamiyoModPack.Kamiyo_Re21341.Passives
{
    public class PassiveAbility_MaskOfPerception_Re21341 : PassiveAbilityBase
    {
        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            if (buf.bufType != KeywordBuf.Paralysis) return base.CanAddBuf(buf);
            owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>().OnAddBuf(1);
            return false;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Evasion) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            UnitUtil.SetPassiveCombatLog(this, owner);
            attackerCard?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel;
            if (card == null)
            {
                battlePlayingCardDataInUnitModel = null;
            }
            else
            {
                var target = card.target;
                battlePlayingCardDataInUnitModel = target?.currentDiceAction;
            }

            var battlePlayingCardDataInUnitModel2 = battlePlayingCardDataInUnitModel;
            UnitUtil.SetPassiveCombatLog(this, owner);
            battlePlayingCardDataInUnitModel2?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }
    }
}