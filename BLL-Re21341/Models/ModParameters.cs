using System.Collections.Generic;
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
        public static readonly List<int> PersonalCardList = new List<int>{1};
        public static readonly List<int> EgoPersonalCardList = new List<int> { 8 };
        public static readonly List<int> SamuraiCardList = new List<int> { 3, 4, 5, 6, 7 };
        public static readonly List<int> KamiyoCardList = new List<int>();
        public static readonly List<int> MioCardList = new List<int>();
        public static readonly List<int> HayateCardList = new List<int>();
        public static readonly Dictionary<int,List<int>> KeypageWithOnlyCardsList = new Dictionary<int, List<int>> { {10000001,new List<int>{3,4,5,6,7}} };
    }
}
