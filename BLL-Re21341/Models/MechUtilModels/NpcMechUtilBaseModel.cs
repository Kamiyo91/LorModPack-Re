namespace BLL_Re21341.Models.MechUtilModels
{
    public class NpcMechUtilBaseModel : MechUtilBaseModel
    {
        public int MechHp { get; set; }
        public bool HasMechOnHp { get; set; }
        public int Counter { get; set; }
        public int MaxCounter { get; set; }
        public bool ReloadMassAttackOnLethal { get; set; }
        public LorId LorIdArrayMass { get; set; } = null;
        public bool MassAttackCount { get; set; } = false;
    }
}
