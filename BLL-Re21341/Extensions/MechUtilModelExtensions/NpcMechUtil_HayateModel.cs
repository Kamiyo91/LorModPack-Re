using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
