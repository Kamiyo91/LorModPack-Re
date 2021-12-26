using System;
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
        public static Dictionary<string, EffectTextModel> EffectTexts = new Dictionary<string, EffectTextModel>();
        public static Dictionary<string, string> NameTexts = new Dictionary<string, string>();

        public static List<Tuple<string, List<int>, int>> OnlyCardKeywords = new List<Tuple<string, List<int>, int>>
        {
            new Tuple<string, List<int>, int>("KamiyoPage_Re21341", new List<int> { 19, 20, 21, 22 }, 10000004),
            new Tuple<string, List<int>, int>("MioPage_Re21341", new List<int> { 12, 13, 14, 15 }, 10000003),
            new Tuple<string, List<int>, int>("HayatePage_Re21341", new List<int> { 23, 24, 25, 26, 27 }, 10000005),
            new Tuple<string, List<int>, int>("SamuraiPage_Re21341", new List<int> { 3, 4, 5, 6, 7 }, 10000001),
            new Tuple<string, List<int>, int>("WiltonPage_Re21341", new List<int> { 44, 45, 46 }, 10000006),
            new Tuple<string, List<int>, int>("RazielPage_Re21341", new List<int> { 52, 53, 54, 55 }, 10000007)
        };

        public static readonly Dictionary<int, int> DynamicNames = new Dictionary<int, int>
        {
            { 10000003, 3 },
            { 10000004, 4 },
            { 10000005, 6 },
            { 10000006, 8 },
            { 10000007, 10 }
        };

        public static readonly List<int> PersonalCardList = new List<int>
            { 1, 9, 17, 28, 29, 31, 32, 33, 34, 35, 42, 47, 57 };

        public static readonly List<int> EgoPersonalCardList = new List<int> { 8, 10, 16, 30, 48, 58, 901 };
        public static readonly List<int> UntransferablePassives = new List<int> { 6, 8, 12, 20, 24, 35, 40 };

        public static List<Tuple<string, List<int>, string>> SkinNameIds = new List<Tuple<string, List<int>, string>>
        {
            new Tuple<string, List<int>, string>("KamiyoMask_Re21341", new List<int> { 10000004, 4 },
                "KamiyoNormal_Re21341"),
            new Tuple<string, List<int>, string>("MioRedEye_Re21341", new List<int> { 10000003, 3 },
                "MioNormalEye_Re21341")
        };

        public static readonly Dictionary<string, List<int>> SpritePreviewChange = new Dictionary<string, List<int>>
        {
            { "MioDefault_Re21341", new List<int> { 10000003 } },
            { "KamiyoDefault_Re21341", new List<int> { 10000004 } },
            { "HayateDefault_Re21341", new List<int> { 10000005 } },
            { "WiltonDefault_Re21341", new List<int> { 10000006 } },
            { "RazielDefault_Re21341", new List<int> { 10000007 } },
            { "FragmentDefault_Re21341", new List<int> { 10000008, 10000009, 10000010, 10000011, 10000012 } },
            { "FragmentBlueDefault_Re21341", new List<int> { 10000013, 10000014 } }
        };

        public static readonly Dictionary<string, List<int>> DefaultSpritePreviewChange =
            new Dictionary<string, List<int>>
            {
                { "Sprites/Books/Thumb/243003", new List<int> { 10000001, 10000002 } }
            };

        public static readonly List<Tuple<int, List<int>, List<SephirahType>, List<string>>> PreBattleUnits =
            new List<Tuple<int, List<int>, List<SephirahType>, List<string>>>
            {
                new Tuple<int, List<int>, List<SephirahType>, List<string>>(6, new List<int> { 10000004, 10000003 },
                    new List<SephirahType> { SephirahType.Keter }, new List<string> { "4", "3" })
            };

        public static readonly Dictionary<LorId, bool> BannedEmotionStages = new Dictionary<LorId, bool>
            { { new LorId(PackageId, 6), false } };

        public static readonly List<SkinNames> SkinParameters = new List<SkinNames>
        {
            new SkinNames
            {
                Name = "Wilton_Re21341",
                SkinParameters = new List<SkinParameters>
                {
                    new SkinParameters
                    {
                        PivotPosX = float.Parse("-27.6"), PivotPosY = float.Parse("-377.0001"),
                        Motion = ActionDetail.Special, FileName = "Special.png"
                    },
                    new SkinParameters
                    {
                        PivotPosX = float.Parse("101.2"), PivotPosY = float.Parse("-326.4001"),
                        Motion = ActionDetail.S1, FileName = "S1.png"
                    }
                }
            }
        };
    }
}