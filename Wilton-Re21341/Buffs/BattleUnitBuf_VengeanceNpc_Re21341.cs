using System.Linq;
using BLL_Re21341.Models;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using Sound;
using UnityEngine;
using Util_Re21341.CommonBuffs;
using Wilton_Re21341.Passives;

namespace Wilton_Re21341.Buffs
{
    public class BattleUnitBuf_VengeanceNpc_Re21341 : BattleUnitBuf
    {
        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();
        public BattleUnitBuf_VengeanceNpc_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        public override bool isAssimilation => true;
        protected override string keywordId => "Vengeance_Re21341";
        protected override string keywordIconId => "RedHood_Rage";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            var effect = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect("8_B/FX_IllusionCard_8_B_Punising",
                1f, _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/SmallBird_StrongAtk");
            foreach (var particle in effect.gameObject.GetComponentsInChildren<ParticleSystem>())
                if (particle.gameObject.name.Contains("Bird") || particle.gameObject.name.Contains("Main")) particle.gameObject.SetActive(false);
            var passive = owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_MysticEyes_Re21341) as
                PassiveAbility_MysticEyes_Re21341;
            passive?.ChangeStacks(2);
            BattleUnitModel unit;
            if (owner.faction == Faction.Enemy)
            {
                unit = UnitUtil.AddNewUnitEnemySide(new UnitModel
                {
                    Id = 9,
                    Pos = 1,
                    EmotionLevel = 3,
                    AddEmotionPassive = false
                }, KamiyoModParameters.PackageId);
            }
            else
            {
                var playerUnitList = BattleObjectManager.instance.GetList(Faction.Player);
                unit = UnitUtil.AddNewUnitPlayerSide(_floor, new UnitModel
                {
                    Id = 9,
                    Name = ModParameters.NameTexts.FirstOrDefault(x => x.Key.Equals("9")).Value + "?",
                    EmotionLevel = 4,
                    Pos = playerUnitList.Count,
                    Sephirah = _floor.Sephirah
                }, KamiyoModParameters.PackageId);
            }

            unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_WillOWispAura_Re21341());
            UnitUtil.RefreshCombatUI();
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }

        public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
        {
            return target != _owner ? 1 : 0;
        }
    }
}