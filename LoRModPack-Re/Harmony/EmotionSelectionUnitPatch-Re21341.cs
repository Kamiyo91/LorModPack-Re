using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using HarmonyLib;

namespace LoRModPack_Re21341.Harmony
{
    [HarmonyPatch]
    public class EmotionSelectionUnitPatch_Re21341
    {
        [HarmonyPostfix]
        public static void LevelUpUI_Predicate_Patch(BattleUnitModel x, ref bool __result)
        {
            __result |= x.UnitData.unitData.bookItem.BookId == new LorId(ModParameters.PackageId, 10000002);
        }
        [HarmonyTargetMethod]
        public static MethodBase LevelUpUI_Predicate_Find()
        {
            TypeInfo t = typeof(LevelUpUI).GetTypeInfo().DeclaredNestedTypes.FirstOrDefault(x => x.Name.Contains("<>c"));
            MethodInfo methodInfo = t.DeclaredMethods.ToList().FirstOrDefault(x => x.Name.Contains("OnSelectRoutine"));
            return methodInfo;
        }
    }
}
