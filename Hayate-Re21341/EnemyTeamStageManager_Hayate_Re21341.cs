using System.Collections.Generic;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Hayate_Re21341
{
    public class EnemyTeamStageManager_Hayate_Re21341 : EnemyTeamStageManager_BaseWithCMU_DLL4221
    {
        public override void OnWaveStart()
        {
            SetParameters(CustomMapHandler.GetCMU(KamiyoModParameters.PackageId), new HayateUtil().HayateNpcUtil,
                new List<MapModel> { KamiyoModParameters.HayateMap });
            base.OnWaveStart();
        }
    }
}