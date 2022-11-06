using System.Collections.Generic;
using BigDLL4221.Models;
using BigDLL4221.StageManagers;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Raziel_Re21341
{
    public class EnemyTeamStageManager_Raziel_Re21341 : EnemyTeamStageManager_BaseWithCMU_DLL4221
    {
        public override void OnWaveStart()
        {
            SetParameters(new RazielUtil().RazielNpcUtil, new List<MapModel> { KamiyoModParameters.RazielMap });
            base.OnWaveStart();
        }
    }
}