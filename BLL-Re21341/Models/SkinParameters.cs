using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BLL_Re21341.Models
{
    public class SkinNames
    {
        public string Name { get; set; }
        public List<SkinParameters> SkinParameters { get; set; }
    }

    public class SkinParameters
    {
        public float PivotPosX { get; set; } = 0;
        public float PivotPosY { get; set; } = 0;
        public float PivotHeadX { get; set; } = 0;
        public float PivotHeadY { get; set; } = 0;
        public float HeadRotation { get; set; } = 0;
        public ActionDetail Motion { get; set; }
        public string FileName { get; set; }
    }
}
