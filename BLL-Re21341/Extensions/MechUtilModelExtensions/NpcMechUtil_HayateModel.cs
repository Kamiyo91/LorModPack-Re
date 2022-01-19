using BLL_Re21341.Models.MechUtilModels;

namespace BLL_Re21341.Extensions.MechUtilModelExtensions
{
    public class NpcMechUtil_HayateModel : NpcMechUtilBaseModel
    {
        public LorId SecondaryMechCard { get; set; }
        public bool FinalMechStart { get; set; }
        public bool SecondMechHpExist { get; set; }
        public int SecondMechHp { get; set; }
        public int DrawBack { get; set; }
        public bool PhaseChanged { get; set; }
        public bool LastPhaseStart { get; set; }
        public bool FinalMech { get; set; }
        public bool SingleUseMech { get; set; }
        public BattleUnitModel FingersnapTarget { get; set; }
    }
}