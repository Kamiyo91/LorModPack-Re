using System.Collections.Generic;
using Sound;
using UnityEngine;

namespace KamiyoModPack.FloorEgoCards_Re21341
{
    public class DiceCardSelfAbility_HayateFloorEgo_Re21341 : DiceCardSelfAbility_EgoOneScene_Re21341
    {
        public override string SkinName => "Hayate_Re21341";
    }

    public class DiceCardAbility_HayateFloorEgoDie_Re21341 : DiceCardAbilityBase
    {
        private readonly List<BattleUnitModel> Units = new List<BattleUnitModel>();

        public override void BeforeRollDice()
        {
            Units.Clear();
            var audioClip = Resources.Load<AudioClip>("StoryResource/SoundEffects/Story/ch1_FingerSnap");
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip(audioClip);
        }

        public override void OnSucceedAreaAttack(BattleUnitModel target)
        {
            Units.Add(target);
        }

        public override void OnAfterAreaAtk(List<BattleUnitModel> damagedList, List<BattleUnitModel> defensedList)
        {
            foreach (var unit in Units)
            {
                unit.TakeDamage(50);
                unit.breakDetail.TakeBreakDamage(50);
            }
        }
    }
}