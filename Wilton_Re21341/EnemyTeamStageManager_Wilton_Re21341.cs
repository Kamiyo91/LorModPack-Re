using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR_XML;
using Util_Re21341;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Wilton_Re21341
{
    public class EnemyTeamStageManager_Wilton_Re21341 :EnemyTeamStageManager
    {
        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Wilton_Re21341", new Wilton_Re21341MapManager(), false, true, 0.5f, 0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
        }
        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }
    }
}
