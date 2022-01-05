using BLL_Re21341.Models;
using HarmonyLib;

namespace EmotionalBurstPassive_Re21341.Buffs
{
    public class BattleUnitBuf_Mock_Re21341 : BattleUnitBuf
    {
        protected override string keywordId => "Mock_Re21341";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    min = -stack,
                    max = -stack
                });
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all)
                ?.SetValue(this, ModParameters.ArtWorks["Mock_Re21341"]);
            typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all)?.SetValue(this, true);
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}