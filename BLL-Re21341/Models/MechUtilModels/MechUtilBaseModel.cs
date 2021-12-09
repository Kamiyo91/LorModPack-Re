using System;

namespace BLL_Re21341.Models.MechUtilModels
{
    public class MechUtilBaseModel
    {
        public BattleUnitModel Owner { get; set; }
        public int Hp { get; set; }
        public int SetHp { get; set; }
        public bool Revive { get; set; }
        public bool Survive { get; set; }
        public bool HasEgo { get; set; }
        public bool EgoActivated { get; set; }
        public bool RefreshUI { get; set; }
        public string SkinName { get; set; }
        public Type EgoType { get; set; }
        public LorId[] LorIdArray { get; set; } = null;
        public LorId EgoCardId { get; set; }
    }
}
