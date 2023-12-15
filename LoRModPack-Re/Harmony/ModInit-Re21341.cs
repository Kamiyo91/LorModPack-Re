using System;
using System.IO;
using System.Linq;
using System.Reflection;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.LoRModPack_Re.Harmony
{
    public class ModInit_Re21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            OtherModCheck();
            KamiyoModParameters.Path =
                Path.GetDirectoryName(
                    Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
        }

        public static void OtherModCheck()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            KamiyoModParameters.EmotionCardUtilLoaded = assemblies.Any(x => x.GetName().Name == "1EmotionCardUtil");
        }
    }
}