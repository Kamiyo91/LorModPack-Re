using System.Linq;

namespace KamiyoModPack.Hayate_Re21341.EmotionCards
{
    public class EmotionCardAbility_Hayate1_21341 : EmotionCardAbilityBase
    {
        public override void OnSelectEmotion()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList().Where(x => x != _owner))
                unit.TakeDamage(50);
        }
    }
}