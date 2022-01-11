using System.Linq;
using BLL_Re21341.Models;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_Solo_Re21341 : PassiveAbilityBase
    {
        private bool _cardUsed;

        public override void OnWaveStart()
        {
            SetCardValue(false);
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 61));
        }

        public override void OnRoundStartAfter()
        {
            if (!_cardUsed) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction).Where(x=> x != owner))
                unit.Die();
        }

        public void SetCardValue(bool value)
        {
            _cardUsed = value;
        }
    }
}