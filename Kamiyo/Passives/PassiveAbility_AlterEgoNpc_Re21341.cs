using BLL_Re21341.Models.MechUtilModels;
using Kamiyo_Re21341.Buffs;
using Util_Re21341.BaseClass;

namespace Kamiyo_Re21341.Passives.Npc
{
    public class PassiveAbility_AlterEgoNpc_Re21341 : PassiveAbilityBase
    {
        private NpcMechUtilBase _util;
        public override void OnWaveStart()
        {
            _util = new NpcMechUtilBase(new NpcMechUtilBaseModel { Hp = 125, SetHp = 125, Survive = true,HasEgo = true,SkinName = "KamiyoMask-Re21341",EgoType = typeof(BattleUnitBuf_AlterEgoRelease_Re21341)});
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.SurviveCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }
    }
}
