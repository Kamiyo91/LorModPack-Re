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
        public static string[] SkinNames = { "KamiyoMask_Re21341", "MioRedEye_Re21341" };
        public static Dictionary<string, string> EffectTexts = new Dictionary<string, string>();
        public static Dictionary<string, string> NameTexts = new Dictionary<string, string>();
        public static readonly List<int> PersonalCardList = new List<int>{1,9,17};
        public static readonly List<int> EgoPersonalCardList = new List<int> { 8,10,16,901 };
        public static readonly List<int> SamuraiCardList = new List<int> { 3, 4, 5, 6, 7 };
        public static readonly List<int> KamiyoCardList = new List<int>{19,20,21,22};
        public static readonly List<int> MioCardList = new List<int>{12,13,14,15};
        public static readonly List<int> HayateCardList = new List<int>();
        public static readonly List<int> UntransferablePassives = new List<int> {6,8,12};
        public static readonly Dictionary<int,List<int>> KeypageWithOnlyCardsList = new Dictionary<int, List<int>> { {10000001,new List<int>{3,4,5,6,7}}, { 10000003, new List<int> {12, 13, 14, 15}},{ 10000004, new List<int> { 19, 20, 21, 22 } } };
    }
}
