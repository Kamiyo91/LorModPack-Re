using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BigDLL4221.Enum;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;
using LOR_DiceSystem;
using MonoMod.Utils;
using UnityEngine;

namespace KamiyoModPack.LoRModPack_Re.Harmony
{
    public class ModInit_Re21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            OnInitParameters();
            ArtUtil.GetArtWorks(new DirectoryInfo(KamiyoModParameters.Path + "/ArtWork"));
            CardUtil.ChangeCardItem(ItemXmlDataList.instance, KamiyoModParameters.PackageId);
            PassiveUtil.ChangePassiveItem(KamiyoModParameters.PackageId);
            LocalizeUtil.AddGlobalLocalize(KamiyoModParameters.PackageId);
            ArtUtil.PreLoadBufIcons();
            LocalizeUtil.RemoveError();
            CardUtil.InitKeywordsList(new List<Assembly> { Assembly.GetExecutingAssembly() });
            ArtUtil.InitCustomEffects(new List<Assembly> { Assembly.GetExecutingAssembly() });
            CustomMapHandler.ModResources.CacheInit.InitCustomMapFiles(Assembly.GetExecutingAssembly());
        }

        private static void OnInitParameters()
        {
            ModParameters.PackageIds.Add(KamiyoModParameters.PackageId);
            KamiyoModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Path.Add(KamiyoModParameters.PackageId, KamiyoModParameters.Path);
            ModParameters.DefaultKeyword.Add(KamiyoModParameters.PackageId, "LoRModPage_Re21341");
            OnInitSprites();
            OnInitSkins();
            OnInitKeypages();
            OnInitCards();
            OnInitPassives();
            OnInitRewards();
            OnInitStages();
            OnInitCredenza();
        }

        private static void OnInitRewards()
        {
            ModParameters.StartUpRewardOptions.Add(new RewardOptions(new Dictionary<LorId, int>
                {
                    { new LorId(KamiyoModParameters.PackageId, 6), 0 }
                }
            ));
        }

        private static void OnInitCards()
        {
            ModParameters.CardOptions.Add(KamiyoModParameters.PackageId, new List<CardOptions>
            {
                new CardOptions(3, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
                new CardOptions(4, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
                new CardOptions(5, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
                new CardOptions(6, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
                new CardOptions(7, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
                new CardOptions(12, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) }),
                new CardOptions(13, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) }),
                new CardOptions(14, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) }),
                new CardOptions(15, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) }),
                new CardOptions(19, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000004) }),
                new CardOptions(20, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000004) }),
                new CardOptions(21, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000004) }),
                new CardOptions(22, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000004) }),
                new CardOptions(23, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
                new CardOptions(24, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
                new CardOptions(25, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
                new CardOptions(26, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
                new CardOptions(27, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
                new CardOptions(43, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) }),
                new CardOptions(44, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) }),
                new CardOptions(45, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) }),
                new CardOptions(46, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) }),
                new CardOptions(52, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
                new CardOptions(53, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
                new CardOptions(54, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
                new CardOptions(55, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
                new CardOptions(8, CardOption.EgoPersonal),
                new CardOptions(10, CardOption.EgoPersonal),
                new CardOptions(16, CardOption.EgoPersonal),
                new CardOptions(48, CardOption.EgoPersonal),
                new CardOptions(58, CardOption.EgoPersonal),
                new CardOptions(1, CardOption.Personal),
                new CardOptions(9, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.white, iconColor: HSVColors.White)),
                new CardOptions(17, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.gray, iconColor: HSVColors.Black)),
                new CardOptions(28, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.blue, iconColor: HSVColors.Blue)),
                new CardOptions(29, CardOption.Personal),
                new CardOptions(42, CardOption.Personal),
                new CardOptions(47, CardOption.Personal),
                new CardOptions(57, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.red, iconColor: new HSVColor(6, 82, 58))),
                new CardOptions(59, CardOption.Personal),
                new CardOptions(60, CardOption.Personal),
                new CardOptions(61, CardOption.Personal),
                new CardOptions(907, CardOption.Personal)
            });
        }

        private static void OnInitSkins()
        {
            ModParameters.SkinOptions.AddRange(new Dictionary<string, SkinOptions>
            {
                { "KamiyoNormal_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
                { "KamiyoMask_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
                { "MioNormalEye_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
                { "MioRedEye_Re21341", new SkinOptions(KamiyoModParameters.PackageId) }
            });
        }

        private static void OnInitKeypages()
        {
            ModParameters.KeypageOptions.Add(KamiyoModParameters.PackageId, new List<KeypageOptions>
            {
                new KeypageOptions(3,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 3, customFaceData: false,
                        originalSkin: "MioNormalEye_Re21341", egoSkin: new List<string> { "MioRedEye_Re21341" })),
                new KeypageOptions(4,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 4, customFaceData: false,
                        originalSkin: "KamiyoNormal_Re21341", egoSkin: new List<string> { "KamiyoMask_Re21341" })),
                new KeypageOptions(10000003, everyoneCanEquip: true,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 3, originalSkin: "MioNormalEye_Re21341",
                        egoSkin: new List<string> { "MioRedEye_Re21341" })),
                new KeypageOptions(10000004, everyoneCanEquip: true,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 4, originalSkin: "KamiyoNormal_Re21341",
                        egoSkin: new List<string> { "KamiyoMask_Re21341" })),
                new KeypageOptions(10000005, everyoneCanEquip: true,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 6)),
                new KeypageOptions(10000006, everyoneCanEquip: true,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 8)),
                new KeypageOptions(10000007, everyoneCanEquip: true,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 10)),
                new KeypageOptions(10000017, keypageColorOptions: new KeypageColorOptions(Color.red, Color.red))
            });
        }

        private static void OnInitCredenza()
        {
            ModParameters.CredenzaOptions.Add(KamiyoModParameters.PackageId,
                new CredenzaOptions(CredenzaEnum.ModifiedCredenza, credenzaNameId: "LorModPackRe21341.Mod",
                    customIconSpriteId: "LorModPackRe21341.Mod", credenzaBooksId: new List<int>
                    {
                        10000001, 10000003, 10000004,
                        10000005, 10000006, 10000007
                    }));
        }

        private static void OnInitSprites()
        {
            ModParameters.SpriteOptions.Add(KamiyoModParameters.PackageId, new List<SpriteOptions>
            {
                new SpriteOptions(SpriteEnum.Base, 10000001, "Sprites/Books/Thumb/243003"),
                new SpriteOptions(SpriteEnum.Base, 10000002, "Sprites/Books/Thumb/243003"),
                new SpriteOptions(SpriteEnum.Custom, 10000003, "MioDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000004, "KamiyoDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000005, "HayateDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000006, "WiltonDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000007, "RazielDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000008, "FragmentDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000011, "FragmentDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000012, "FragmentDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000013, "FragmentBlueDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000014, "FragmentBlueDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000015, "FragmentBlueDefault_Re21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000016, "FragmentRedDefault_Re21341")
            });
        }

        private static void OnInitStages()
        {
            ModParameters.StageOptions.Add(KamiyoModParameters.PackageId, new List<StageOptions>
            {
                new StageOptions(1, preBattleOptions: new PreBattleOptions(onlySephirah: true)),
                new StageOptions(4, preBattleOptions: new PreBattleOptions(onlySephirah: true)),
                new StageOptions(6, true,
                    preBattleOptions: new PreBattleOptions(new List<SephirahType> { SephirahType.Keter },
                        new List<UnitModel>
                            { KamiyoModParameters.KamiyoPreBattleModel, KamiyoModParameters.MioPreBattleModel })),
                new StageOptions(11, true)
            });
        }

        private static void OnInitPassives()
        {
            ModParameters.PassiveOptions.Add(KamiyoModParameters.PackageId, new List<PassiveOptions>
            {
                new PassiveOptions(6, false),
                new PassiveOptions(7, false, passiveColorOptions: new PassiveColorOptions(Color.white, Color.white)),
                new PassiveOptions(8, false, passiveColorOptions: new PassiveColorOptions(Color.white, Color.white)),
                new PassiveOptions(12, false, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(13, false, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(18, false, passiveColorOptions: new PassiveColorOptions(Color.blue, Color.blue)),
                new PassiveOptions(20, false, passiveColorOptions: new PassiveColorOptions(Color.blue, Color.blue)),
                new PassiveOptions(24, false),
                new PassiveOptions(35, false),
                new PassiveOptions(38, false, passiveColorOptions: new PassiveColorOptions(Color.yellow, Color.yellow)),
                new PassiveOptions(40, false, passiveColorOptions: new PassiveColorOptions(Color.yellow, Color.yellow)),
                new PassiveOptions(58, false),
                new PassiveOptions(57, bannedEmotionCardSelection: true, bannedEgoFloorCards: true),
                new PassiveOptions(37,
                    canBeUsedWithPassivesOne: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 8) }),
                new PassiveOptions(17, passiveColorOptions: new PassiveColorOptions(Color.white, Color.white),
                    canBeUsedWithPassivesOne: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 12) }),
                new PassiveOptions(26,
                    canBeUsedWithPassivesOne: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 20) }),
                new PassiveOptions(61, canBeUsedWithPassivesOne: new List<LorId>
                {
                    new LorId(KamiyoModParameters.PackageId, 6), new LorId(KamiyoModParameters.PackageId, 8),
                    new LorId(KamiyoModParameters.PackageId, 12),
                    new LorId(KamiyoModParameters.PackageId, 20), new LorId(KamiyoModParameters.PackageId, 24),
                    new LorId(KamiyoModParameters.PackageId, 40),
                    new LorId(KamiyoModParameters.SephirahBundlePackageId, 27)
                }, passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
                new PassiveOptions(21, cannotBeUsedWithPassives: new List<LorId> { new LorId(250115) }),
                new PassiveOptions(37, passiveColorOptions: new PassiveColorOptions(Color.red, Color.red))
            });
        }
    }
}
//ModParameters.SkinParameters.AddRange(new List<SkinNames>
//{
//    new SkinNames
//    {
//        PackageId = KamiyoModParameters.PackageId,
//        Name = "Wilton_Re21341",
//        SkinParameters = new List<SkinParameters>
//        {
//            new SkinParameters
//            {
//                PivotPosX = float.Parse("-28"), PivotPosY = float.Parse("-377"),
//                Motion = ActionDetail.Special, FileName = "Special.png"
//            },
//            new SkinParameters
//            {
//                PivotPosX = float.Parse("101"), PivotPosY = float.Parse("-326"),
//                Motion = ActionDetail.S1, FileName = "S1.png"
//            }
//        }
//    }
//});

//new Tuple<LorId, List<LorId>>(new LorId(KamiyoModParameters.PackageId, 22),
//    new List<LorId>
//    {
//        new LorId("SaeModSa21341.Mod", 3), new LorId("SaeModSa21341.Mod", 8),
//        new LorId("MaryIb21341.Mod", 3)
//    }),