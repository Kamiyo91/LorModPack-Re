using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models;
using Omori_Re21341.MapManagers;
using Util_Re21341;
using Util_Re21341.BaseClass;
using Util_Re21341.CommonBuffs;

namespace Omori_Re21341.MechUtil
{
    public class MechUtil_Omori : MechUtilBase
    {
        private readonly MechUtil_OmoriModel _model;

        public MechUtil_Omori(MechUtil_OmoriModel model) : base(model)
        {
            _model = model;
        }

        public override void SurviveCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.Hp || !_model.Survive) return;
            _model.Survive = false;
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEnd_Re21341());
            _model.Owner.SetHp(_model.SetHp);
            UnitUtil.UnitReviveAndRecovery(_model.Owner, 0, _model.RecoverLightOnSurvive);
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmunityToStatusAliment_Re21341());
            SetSuccumbStatus(true);
        }

        public bool GetSuccumbStatus()
        {
            return _model.NotSuccumb;
        }

        public void SetSuccumbStatus(bool value)
        {
            _model.NotSuccumb = value;
        }

        public void ChangeMap(BattleUnitModel owner)
        {
            if (owner.faction != Faction.Player ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            _model.MapChanged = true;
            ChangeToOmoriEgoMap();
        }

        private static void ChangeToOmoriEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "Omori5_Re21341",
                StageId = 8,
                IsPlayer = true,
                OneTurnEgo = true,
                Component = new Omori5_Re21341MapManager(),
                Bgy = 0.45f
            });
        }

        public void ReturnFromEgoMap()
        {
            if (!_model.MapChanged) return;
            _model.MapChanged = false;
            MapUtil.ReturnFromEgoMap("Omori5_Re21341", 8);
        }
    }
}