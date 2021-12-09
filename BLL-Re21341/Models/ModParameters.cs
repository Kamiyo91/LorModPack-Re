using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BLL_Re21341.Models
{
    public static class ModParameters
    {
        public const string PackageId = "LorModPackRe21341.Mod";
        public static string Path;
        public static readonly Dictionary<string, Sprite> ArtWorks = new Dictionary<string, Sprite>();
        public static string Language;
        public static string[] SkinNames = { "KamiyoMask-Re21341", "MioRedEye-Re21341" };
        public static Dictionary<string, string> EffectTexts = new Dictionary<string, string>();
    }
}
