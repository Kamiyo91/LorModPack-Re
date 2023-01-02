using System.Collections.Generic;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Kamiyo_Re21341
{
    public class EnemyTeamStageManager_Kamiyo_Re21341 : EnemyTeamStageManager_BaseWithCMU_DLL4221
    {
        public override void OnWaveStart()
        {
            SetParameters(CustomMapHandler.GetCMU(KamiyoModParameters.PackageId), new KamiyoUtil().KamiyoNpcUtil,
                new List<MapModel> { KamiyoModParameters.KamiyoMap1, KamiyoModParameters.KamiyoMap2 });
            base.OnWaveStart();
        }
    }
}