using System.Collections.Generic;
using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Extensions;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;
using LOR_XML;

namespace KamiyoModPack.Hayate_Re21341.MechUtil
{
    public class NpcMechUtil_Hayate : NpcMechUtilBase
    {
        private List<BattleEmotionCardModel> _emotionCards = new List<BattleEmotionCardModel>();
        private BattleUnitModel _fingersnapSpecialTarget;
        public bool WiltonCase;

        public NpcMechUtil_Hayate(NpcMechUtilBaseModel model) : base(model)
        {
            WiltonCase = false;
        }

        public override void ExtraMethodOnRoundEndTheLast()
        {
            if (_fingersnapSpecialTarget != null)
            {
                BattleObjectManager.instance.UnregisterUnit(_fingersnapSpecialTarget);
                _fingersnapSpecialTarget = null;
                UnitUtil.RefreshCombatUI();
            }

            if (Model.Phase > 2 || !Model.Owner.IsDead()) return;
            UnitUtil.UnitReviveAndRecovery(Model.Owner, 5, false);
        }

        public override bool OnUseMechBuffAttackCard(BattlePlayingCardDataInUnitModel card)
        {
            if (!base.OnUseMechBuffAttackCard(card)) return base.OnUseMechBuffAttackCard(card);
            if (card.card.GetID() == new LorId(KamiyoModParameters.PackageId, 904))
                return base.OnUseMechBuffAttackCard(card);
            _fingersnapSpecialTarget = card.target;
            return base.OnUseMechBuffAttackCard(card);
        }

        public override bool SpecialChangePhaseCondition()
        {
            if (Model.Phase > 0) return false;
            var unit = BattleObjectManager.instance.GetAliveList(UnitUtil.ReturnOtherSideFaction(Model.Owner.faction))
                .FirstOrDefault();
            return unit != null && unit.hp < unit.MaxHp * 0.7f;
        }

        public override void ExtraMethodOnOtherUnitDie(BattleUnitModel unit)
        {
            _emotionCards = UnitUtil.AddValueToEmotionCardList(UnitUtil.GetEmotionCardByUnit(unit), _emotionCards);
        }

        public override void ExtraMechRoundPreEnd(MechPhaseOptions mechOptions)
        {
            foreach (var unit in BattleObjectManager.instance.GetList(
                         UnitUtil.ReturnOtherSideFaction(Model.Owner.faction)))
                BattleObjectManager.instance.UnregisterUnit(unit);
            var kamiyoUnit = UnitUtil.AddNewUnitWithDefaultData(KamiyoModParameters.KamiyoSoloUnit, 0,
                emotionLevel: Model.Owner.emotionDetail.EmotionLevel);
            if (kamiyoUnit.HasPassivePlayerMech(out var passive))
            {
                passive.Util.ChangeEgoAbDialog(new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Kamiyo",
                        dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                            .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive4_Re21341"))
                            .Value.Desc
                    }
                });
                passive.ForcedEgo(0);
                passive.SetDieAtEnd();
            }

            UnitUtil.RefreshCombatUI();
            UnitUtil.ChangeCardCostByValue(kamiyoUnit, -5, 6, false);
            kamiyoUnit.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 43));
            var specialPassive = kamiyoUnit.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 17));
            specialPassive.OnWaveStart();
            UnitUtil.UnitReviveAndRecovery(Model.Owner, 50, true);
            UnitUtil.ApplyEmotionCards(kamiyoUnit, _emotionCards);
        }

        public override void ExtraMethodCase()
        {
            WiltonCase = true;
        }

        public override void ExtraMethodOnKill(BattleUnitModel unit)
        {
            if (!WiltonCase) return;
            var playerUnit = BattleObjectManager.instance.GetAliveList(Faction.Player).FirstOrDefault();
            if (playerUnit != null)
            {
                playerUnit.forceRetreat = true;
                playerUnit.Die();
            }

            Model.Owner.DieFake();
        }
    }
}