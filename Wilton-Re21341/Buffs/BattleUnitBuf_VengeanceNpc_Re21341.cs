using System.Linq;
using Battle.CreatureEffect;
using BLL_Re21341.Models;
using Sound;
using UnityEngine;
using Util_Re21341;
using Util_Re21341.CommonBuffs;
using Wilton_Re21341.Passives;

namespace Wilton_Re21341.Buffs
{
    public class BattleUnitBuf_VengeanceNpc_Re21341 : BattleUnitBuf
    {
        private const string Path = "6/RedHood_Emotion_Aura";

        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();

        private CreatureEffect _aura;

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
            PlayChangingEffect(owner);
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
                });
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
                });
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

        private void PlayChangingEffect(BattleUnitModel owner)
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            if (_aura == null)
                _aura = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect(Path, 1f, owner.view,
                    owner.view);
            var original = Resources.Load("Prefabs/Battle/SpecialEffect/RedMistRelease_ActivateParticle");
            if (original != null)
            {
                var gameObject = Object.Instantiate(original) as GameObject;
                gameObject.transform.parent = owner.view.charAppearance.transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.transform.localScale = Vector3.one;
            }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

        public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
        {
            return target != _owner ? 1 : 0;
        }
    }
}