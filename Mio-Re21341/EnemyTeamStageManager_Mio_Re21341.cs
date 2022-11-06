using System.Collections.Generic;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Mio_Re21341
{
    public class EnemyTeamStageManager_Mio_Re21341 : EnemyTeamStageManager_BaseWithCMU_DLL4221
    {
        public override void OnWaveStart()
        {
            SetParameters(new MioUtil().MioNpcUtil, new List<MapModel> { KamiyoModParameters.MioMap });
            base.OnWaveStart();
        }
    }
}