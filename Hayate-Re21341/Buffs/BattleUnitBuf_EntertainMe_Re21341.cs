using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using HarmonyLib;

namespace Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_EntertainMe_Re21341 : BattleUnitBuf
    {
        private int _addValue;
        public override BufPositiveType positiveType => BufPositiveType.Positive;

        protected override string keywordId =>
            _owner.faction == Faction.Player ? "EntertainMePlayer_Re21341" : "EntertainMeNpc_Re21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all)
                ?.SetValue(this, ModParameters.ArtWorks["EntertainMe_Re21341"]);
            typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all)?.SetValue(this, true);
            _addValue = 1;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (stack >= 40)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = 1
                    });
        }

        public override void OnRoundEndTheLast()
        {
            AddStack(_owner.faction == Faction.Enemy ? 5 : 3);
        }

        private void AddStack(int value = 1)
        {
            if (stack + _addValue > 50)
                stack = 50;
            else
                stack += _addValue * value;
        }

        private void SubStack()
        {
            if (stack - _addValue < 0)
                stack = 0;
            else
                stack -= _addValue;
        }
        public void SetValue(int value)
        {
            _addValue = value;
        }
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            AddStack();
        }

        public override void BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (attacker == null) return;
            SubStack();
        }
    }
}
