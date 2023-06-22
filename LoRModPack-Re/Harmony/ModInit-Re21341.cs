using System;
using System.IO;
using System.Reflection;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.LoRModPack_Re.Harmony
{
    public class ModInit_Re21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            KamiyoModParameters.Path =
                Path.GetDirectoryName(
                    Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
        }
    }
}