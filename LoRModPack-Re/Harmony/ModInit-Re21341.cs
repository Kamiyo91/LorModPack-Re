using System;
using System.IO;
using System.Reflection;
using BLL_Re21341.Models;
using Util_Re21341;

namespace LoRModPack_Re21341.Harmony
{
    public class ModInit_Re21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            ModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            new HarmonyLib.Harmony("LOR.LorModPackRe21341_MOD").PatchAll();
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            MapUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance);
            UnitUtil.ChangePassiveItem();
            SkinUtil.LoadBookSkinsExtra();
            SkinUtil.PreLoadBufIcons();
            LocalizeUtil.AddLocalize();
            LocalizeUtil.RemoveError();
        }
    }
}