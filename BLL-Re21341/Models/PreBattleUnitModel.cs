using System.Collections.Generic;

namespace BLL_Re21341.Models
{
    public class PreBattleUnitModel
    {
        public int UnitId { get; set; }
        public SephirahType SephirahUnit { get; set; }
        public string UnitNameId { get; set; }
        public string SkinName { get; set; }
        public List<LorId> PassiveIds { get; set; }
    }
    //new List<Tuple<int, List<int>, List<SephirahType>, List<string>, List<string>>>
    //{
    //new Tuple<int, List<int>, List<SephirahType>, List<string>, List<string>>(6,
    //new List<int> { 10000004, 10000003 },
    //new List<SephirahType> { SephirahType.Keter }, new List<string> { "4", "3" },
    //new List<string> { "KamiyoNormal_Re21341", "MioNormalEye_Re21341" })
    //};
}