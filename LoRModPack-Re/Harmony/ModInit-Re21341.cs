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
        //Old Version
        //public override void OnInitializeMod()
        //{
        //    OnInitParameters();
        //    ArtUtil.GetArtWorks(new DirectoryInfo(KamiyoModParameters.Path + "/ArtWork"));
        //    ArtUtil.GetSpeedDieArtWorks(new DirectoryInfo(KamiyoModParameters.Path + "/CustomDiceArtWork"));
        //    CardUtil.ChangeCardItem(ItemXmlDataList.instance, KamiyoModParameters.PackageId);
        //    PassiveUtil.ChangePassiveItem(KamiyoModParameters.PackageId);
        //    KeypageUtil.ChangeKeypageItem(BookXmlList.Instance, KamiyoModParameters.PackageId);
        //    CardUtil.LoadEmotionAndEgoCards(KamiyoModParameters.PackageId, KamiyoModParameters.Path + "/EmotionCards",
        //        new List<Assembly> { Assembly.GetExecutingAssembly() });
        //    CardUtil.SetFloorPullCodeCards(KamiyoModParameters.PackageId, KamiyoModParameters.PoolCode,
        //        TypeCardEnum.Emotion, KamiyoModParameters.EmotionCardIds);
        //    CardUtil.SetFloorPullCodeCards(KamiyoModParameters.PackageId, KamiyoModParameters.PoolCode,
        //        TypeCardEnum.Ego, KamiyoModParameters.FloorEgoCardIds);
        //    CardUtil.SetEmotionCardColors(KamiyoModParameters.PackageId, new List<int> { 21344, 21347 },
        //        new EmotionCardColorOptions(Color.white, Color.white, HSVColors.White));
        //    LocalizeUtil.AddGlobalLocalize(KamiyoModParameters.PackageId);
        //    ArtUtil.MakeCustomBook(KamiyoModParameters.PackageId);
        //    ArtUtil.PreLoadBufIcons();
        //    LocalizeUtil.RemoveError();
        //    CardUtil.InitKeywordsList(new List<Assembly> { Assembly.GetExecutingAssembly() });
        //    ArtUtil.InitCustomEffects(new List<Assembly> { Assembly.GetExecutingAssembly() });
        //    CustomMapHandler.ModResources.CacheInit.InitCustomMapFiles(Assembly.GetExecutingAssembly());
        //}

        //private static void OnInitParameters()
        //{
        //    ModParameters.PackageIds.Add(KamiyoModParameters.PackageId);
        //    KamiyoModParameters.Path = Path.GetDirectoryName(
        //        Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
        //    ModParameters.Path.Add(KamiyoModParameters.PackageId, KamiyoModParameters.Path);
        //    ModParameters.Assemblies.Add(Assembly.GetExecutingAssembly());
        //    ModParameters.DefaultKeyword.Add(KamiyoModParameters.PackageId, "LoRModPage_Re21341");
        //    OnInitSprites();
        //    OnInitSkins();
        //    OnInitKeypages();
        //    OnInitCards();
        //    OnInitDropBooks();
        //    OnInitPassives();
        //    OnInitCategories();
        //    OnInitRewards();
        //    OnInitStages();
        //    OnInitCredenza();
        //    OnInitCustomSkins();
        //}

        //private static void OnInitCustomSkins()
        //{
        //    ModParameters.CustomBookSkinsOptions.Add(KamiyoModParameters.PackageId, new List<CustomBookSkinsOption>
        //    {
        //        new CustomBookSkinsOption("MioNormalEye_Re21341", 10000003, characterNameId: 3),
        //        new CustomBookSkinsOption("KamiyoNormal_Re21341", 10000004, characterNameId: 4),
        //        new CustomBookSkinsOption("Raziel_Re21341", 10000007, characterNameId: 10),
        //        new CustomBookSkinsOption("Hayate_Re21341", 10000005, characterNameId: 6),
        //        new CustomBookSkinsOption("Wilton_Re21341", 10000006, characterNameId: 8)
        //    });
        //}

        //private static void OnInitRewards()
        //{
        //    ModParameters.StartUpRewardOptions.Add(new RewardOptions(new Dictionary<LorId, int>
        //        {
        //            { new LorId(KamiyoModParameters.PackageId, 6), 0 }
        //        }
        //    ));
        //}

        //private static void OnInitCards()
        //{
        //    ModParameters.CardOptions.Add(KamiyoModParameters.PackageId, new List<CardOptions>
        //    {
        //        new CardOptions(3, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
        //        new CardOptions(4, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
        //        new CardOptions(5, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
        //        new CardOptions(6, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
        //        new CardOptions(7, CardOption.OnlyPage, new List<string> { "SamuraiPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000001) }),
        //        new CardOptions(12, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) },
        //            cardColorOptions: new CardColorOptions(Color.white, iconColor: HSVColors.White)),
        //        new CardOptions(13, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) }),
        //        new CardOptions(14, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) }),
        //        new CardOptions(15, CardOption.OnlyPage, new List<string> { "MioPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) }),
        //        new CardOptions(19, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
        //            new List<LorId>
        //            {
        //                new LorId(KamiyoModParameters.PackageId, 10000004),
        //                new LorId(KamiyoModParameters.VortexModPackageId, 10000005)
        //            }),
        //        new CardOptions(20, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
        //            new List<LorId>
        //            {
        //                new LorId(KamiyoModParameters.PackageId, 10000004),
        //                new LorId(KamiyoModParameters.VortexModPackageId, 10000005)
        //            }),
        //        new CardOptions(21, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
        //            new List<LorId>
        //            {
        //                new LorId(KamiyoModParameters.PackageId, 10000004),
        //                new LorId(KamiyoModParameters.VortexModPackageId, 10000005)
        //            }),
        //        new CardOptions(22, CardOption.OnlyPage, new List<string> { "KamiyoPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000004) },
        //            cardColorOptions: new CardColorOptions(Color.gray, customIconColor: new Color(0.5f, 0.5f, 0.5f),
        //                iconColor: HSVColors.Black)),
        //        new CardOptions(23, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
        //        new CardOptions(24, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
        //        new CardOptions(25, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) },
        //            cardColorOptions: new CardColorOptions(Color.gray, customIconColor: new Color(0.5f, 0.5f, 0.5f),
        //                iconColor: HSVColors.Black)),
        //        new CardOptions(26, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) },
        //            cardColorOptions: new CardColorOptions(Color.gray, customIconColor: new Color(0.5f, 0.5f, 0.5f),
        //                iconColor: HSVColors.Black)),
        //        new CardOptions(27, CardOption.OnlyPage, new List<string> { "HayatePage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) }),
        //        new CardOptions(43, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) },
        //            cardColorOptions: new CardColorOptions(Color.cyan, customIconColor: Color.cyan,
        //                useHSVFilter: false)),
        //        new CardOptions(44, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) }),
        //        new CardOptions(45, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) }),
        //        new CardOptions(46, CardOption.OnlyPage, new List<string> { "WiltonPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) }),
        //        new CardOptions(52, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
        //        new CardOptions(53, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
        //        new CardOptions(54, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
        //        new CardOptions(55, CardOption.OnlyPage, new List<string> { "RazielPage_Re21341" },
        //            new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) }),
        //        new CardOptions(8, CardOption.EgoPersonal),
        //        new CardOptions(10, CardOption.EgoPersonal),
        //        new CardOptions(16, CardOption.EgoPersonal),
        //        new CardOptions(48, CardOption.EgoPersonal),
        //        new CardOptions(58, CardOption.EgoPersonal),
        //        new CardOptions(1, CardOption.Personal),
        //        new CardOptions(9, CardOption.Personal,
        //            cardColorOptions: new CardColorOptions(Color.white, iconColor: HSVColors.White)),
        //        new CardOptions(17, CardOption.Personal,
        //            cardColorOptions: new CardColorOptions(Color.gray, customIconColor: new Color(0.5f, 0.5f, 0.5f),
        //                iconColor: HSVColors.Black)),
        //        new CardOptions(28, CardOption.Personal,
        //            cardColorOptions: new CardColorOptions(Color.gray, customIconColor: new Color(0.5f, 0.5f, 0.5f),
        //                iconColor: HSVColors.Black)),
        //        new CardOptions(29, CardOption.Personal),
        //        new CardOptions(30, CardOption.Personal, cardColorOptions: new CardColorOptions(Color.red,
        //            customIconColor: Color.red,
        //            useHSVFilter: false)),
        //        new CardOptions(42, CardOption.Personal),
        //        new CardOptions(47, CardOption.Personal,
        //            cardColorOptions: new CardColorOptions(Color.red, customIconColor: Color.red, useHSVFilter: false)),
        //        new CardOptions(57, CardOption.Personal,
        //            cardColorOptions: new CardColorOptions(Color.red, customIconColor: Color.red, useHSVFilter: false)),
        //        new CardOptions(59, CardOption.Personal,
        //            cardColorOptions: new CardColorOptions(Color.white, iconColor: HSVColors.White)),
        //        new CardOptions(60, CardOption.Personal, cardColorOptions: new CardColorOptions(Color.gray,
        //            customIconColor: new Color(0.5f, 0.5f, 0.5f),
        //            iconColor: HSVColors.Black)),
        //        new CardOptions(36, CardOption.Personal),
        //        new CardOptions(61, CardOption.Personal),
        //        new CardOptions(907, CardOption.Personal),
        //        new CardOptions(21356, CardOption.EGO),
        //        new CardOptions(21357, CardOption.EGO),
        //        new CardOptions(21358, CardOption.EGO),
        //        new CardOptions(21359, CardOption.EGO),
        //        new CardOptions(21360, CardOption.EGO)
        //    });
        //}

        //private static void OnInitSkins()
        //{
        //    ModParameters.SkinOptions.AddRange(new Dictionary<string, SkinOptions>
        //    {
        //        { "Wilton_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
        //        { "Hayate_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
        //        { "Raziel_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
        //        { "KamiyoNormal_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
        //        { "KamiyoMask_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
        //        { "MioNormalEye_Re21341", new SkinOptions(KamiyoModParameters.PackageId) },
        //        { "MioRedEye_Re21341", new SkinOptions(KamiyoModParameters.PackageId) }
        //    });
        //}

        //private static void OnInitKeypages()
        //{
        //    ModParameters.KeypageOptions.Add(KamiyoModParameters.PackageId, new List<KeypageOptions>
        //    {
        //        new KeypageOptions(3,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 3, customFaceData: false,
        //                originalSkin: "MioNormalEye_Re21341", egoSkin: new List<string> { "MioRedEye_Re21341" }),
        //            keypageColorOptions: new KeypageColorOptions(Color.white, Color.white)),
        //        new KeypageOptions(4,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 4, customFaceData: false,
        //                originalSkin: "KamiyoNormal_Re21341", egoSkin: new List<string> { "KamiyoMask_Re21341" },
        //                xiaoTaotieAction: ActionDetail.Guard)),
        //        new KeypageOptions(6,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 6),
        //            keypageColorOptions: new KeypageColorOptions(Color.gray,
        //                Color.gray)),
        //        new KeypageOptions(8,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 8),
        //            keypageColorOptions: new KeypageColorOptions(Color.cyan, Color.cyan)),
        //        new KeypageOptions(10,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 10),
        //            keypageColorOptions: new KeypageColorOptions(Color.yellow, Color.yellow)),
        //        new KeypageOptions(10000003, everyoneCanEquip: true,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 3, originalSkin: "MioNormalEye_Re21341",
        //                egoSkin: new List<string> { "MioRedEye_Re21341" }),
        //            keypageColorOptions: new KeypageColorOptions(Color.white, Color.white)),
        //        new KeypageOptions(10000004, everyoneCanEquip: true,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 4, originalSkin: "KamiyoNormal_Re21341",
        //                egoSkin: new List<string> { "KamiyoMask_Re21341" }, xiaoTaotieAction: ActionDetail.Guard),
        //            keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
        //        new KeypageOptions(10000900, bannedEmotionCards: true, bannedEgoFloorCards: true,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 3, originalSkin: "MioNormalEye_Re21341",
        //                egoSkin: new List<string> { "MioRedEye_Re21341" }),
        //            keypageColorOptions: new KeypageColorOptions(Color.white, Color.white)),
        //        new KeypageOptions(10000901, bannedEmotionCards: true, bannedEgoFloorCards: true,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 4, originalSkin: "KamiyoNormal_Re21341",
        //                egoSkin: new List<string> { "KamiyoMask_Re21341" }, xiaoTaotieAction: ActionDetail.Guard),
        //            keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
        //        new KeypageOptions(10000005, everyoneCanEquip: true,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 6),
        //            keypageColorOptions: new KeypageColorOptions(Color.gray,
        //                Color.gray)),
        //        new KeypageOptions(10000002,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 2, customFaceData: false), isDeckFixed: true),
        //        new KeypageOptions(10000009,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 2, customFaceData: false), isDeckFixed: true),
        //        new KeypageOptions(10000006, everyoneCanEquip: true,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 8),
        //            keypageColorOptions: new KeypageColorOptions(Color.cyan, Color.cyan)),
        //        new KeypageOptions(10000007, everyoneCanEquip: true,
        //            bookCustomOptions: new BookCustomOptions(nameTextId: 10),
        //            keypageColorOptions: new KeypageColorOptions(Color.yellow, Color.yellow),
        //            customFloorOptions: new CustomFloorOptions(KamiyoModParameters.PackageId,
        //                KamiyoModParameters.PoolCode,
        //                KamiyoModParameters.PackageId, floorNameId: "KurosawaFloor_21341",
        //                customFloorMap: KamiyoModParameters.HayateSephirahMap,
        //                customDiceColorOptions: new CustomDiceColorOptions("KurosawaFloor_Re21341", Color.white))),
        //        new KeypageOptions(10000017, keypageColorOptions: new KeypageColorOptions(Color.red, Color.red)),
        //        new KeypageOptions(10000018, keypageColorOptions: new KeypageColorOptions(Color.red, Color.red))
        //    });
        //}

        //private static void OnInitCategories()
        //{
        //    ModParameters.CategoryOptions.Add(KamiyoModParameters.PackageId, new List<CategoryOptions>
        //    {
        //        new CategoryOptions(KamiyoModParameters.PackageId, "_1",
        //            bookDataColor: new CategoryColorOptions(Color.white, Color.white)
        //            , categoryBooksId: new List<int> { 10000003, 10000004, 10000005, 10000006, 10000007 },
        //            credenzaBooksId: new List<int> { 10000003, 10000004, 10000005, 10000006, 10000007 },
        //            categoryNameId: "KurosawaSlot1_21341", customIconSpriteId: KamiyoModParameters.PackageId),
        //        new CategoryOptions(KamiyoModParameters.PackageId, "_2",
        //            bookDataColor: new CategoryColorOptions(Color.white, Color.white)
        //            , categoryBooksId: new List<int> { 10000008, 10000011, 10000012, 10000013, 10000014, 10000015, 10000016, 10000017, 10000018 },
        //            categoryNameId: "KurosawaSlot3_21341", customIconSpriteId: KamiyoModParameters.PackageId,
        //            credenzaType: CredenzaEnum.NoCredenza),
        //        new CategoryOptions(KamiyoModParameters.PackageId, "_3"
        //            , new List<int> { 10000001, 10000002, 10000009 }, categoryNameId: "KurosawaSlot2_21341",
        //            credenzaBooksId: new List<int> { 10000001 },
        //            baseIconSpriteId: "Chapter5")
        //    });
        //}

        //private static void OnInitCredenza()
        //{
        //    ModParameters.CredenzaOptions.Add(KamiyoModParameters.PackageId,
        //        new CredenzaOptions(CredenzaEnum.ModifiedCredenza, credenzaNameId: "LorModPackRe21341.Mod",
        //            customIconSpriteId: "LorModPackRe21341.Mod", credenzaBooksId: new List<int>
        //            {
        //                10000001, 10000003, 10000004,
        //                10000005, 10000006, 10000007
        //            }));
        //}

        //private static void OnInitSprites()
        //{
        //    ModParameters.SpriteOptions.Add(KamiyoModParameters.PackageId, new List<SpriteOptions>
        //    {
        //        new SpriteOptions(SpriteEnum.Base, 10000001, "Sprites/Books/Thumb/243003"),
        //        new SpriteOptions(SpriteEnum.Base, 10000002, "Sprites/Books/Thumb/243003"),
        //        new SpriteOptions(SpriteEnum.Base, 10000009, "Sprites/Books/Thumb/243003"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000003, "MioDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000004, "KamiyoDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000005, "HayateDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000006, "WiltonDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000007, "RazielDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000008, "FragmentDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000011, "FragmentDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000012, "FragmentDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000013, "FragmentBlueDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000014, "FragmentBlueDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000015, "FragmentBlueDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000016, "FragmentRedDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000017, "FragmentRedDefault_Re21341"),
        //        new SpriteOptions(SpriteEnum.Custom, 10000018, "FragmentRedDefault_Re21341")
        //    });
        //}

        //private static void OnInitStages()
        //{
        //    ModParameters.StageOptions.Add(KamiyoModParameters.PackageId, new List<StageOptions>
        //    {
        //        new StageOptions(1, preBattleOptions: new PreBattleOptions(onlySephirah: true)),
        //        new StageOptions(2, stageColorOptions: new StageColorOptions(Color.white, Color.white)),
        //        new StageOptions(3, stageColorOptions: new StageColorOptions(Color.gray, Color.gray)),
        //        new StageOptions(4, preBattleOptions: new PreBattleOptions(onlySephirah: true),
        //            stageColorOptions: new StageColorOptions(Color.gray, Color.gray)),
        //        new StageOptions(6, true,
        //            preBattleOptions: new PreBattleOptions(battleType: PreBattleType.CustomUnits,
        //                customUnits: new Dictionary<SephirahType, List<UnitModel>>
        //                {
        //                    {
        //                        SephirahType.Keter, new List<UnitModel>
        //                            { KamiyoModParameters.KamiyoPreBattleModel, KamiyoModParameters.MioPreBattleModel }
        //                    }
        //                }),
        //            stageColorOptions: new StageColorOptions(Color.cyan, Color.cyan)),
        //        new StageOptions(7, stageColorOptions: new StageColorOptions(Color.yellow, Color.yellow),
        //            stageRewardOptions: new RewardOptions(null, new Dictionary<LorId, int>
        //                {
        //                    { new LorId(KamiyoModParameters.PackageId, 52), 3 },
        //                    { new LorId(KamiyoModParameters.PackageId, 53), 3 },
        //                    { new LorId(KamiyoModParameters.PackageId, 54), 1 },
        //                    { new LorId(KamiyoModParameters.PackageId, 55), 3 }
        //                },
        //                new List<LorId>
        //                {
        //                    new LorId(KamiyoModParameters.PackageId, 10000007),
        //                    new LorId(KamiyoModParameters.PackageId, 10000011),
        //                    new LorId(KamiyoModParameters.PackageId, 10000017)
        //                }, "RazielStageReward_Re21341", true)),
        //        new StageOptions(11, true, stageColorOptions: new StageColorOptions(Color.cyan, Color.cyan))
        //    });
        //}

        //private static void OnInitPassives()
        //{
        //    ModParameters.PassiveOptions.Add(KamiyoModParameters.PackageId, new List<PassiveOptions>
        //    {
        //        new PassiveOptions(6, false),
        //        new PassiveOptions(57, false, bannedEgoFloorCards: true, bannedEmotionCardSelection: true),
        //        new PassiveOptions(7, false, passiveColorOptions: new PassiveColorOptions(Color.white, Color.white)),
        //        new PassiveOptions(8, false, passiveColorOptions: new PassiveColorOptions(Color.white, Color.white)),
        //        new PassiveOptions(12, false, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
        //        new PassiveOptions(13, false, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
        //        new PassiveOptions(18, false,
        //            passiveColorOptions: new PassiveColorOptions(Color.gray,
        //                Color.gray)),
        //        new PassiveOptions(20, false,
        //            passiveColorOptions: new PassiveColorOptions(Color.gray,
        //                Color.gray)),
        //        new PassiveOptions(24, false, passiveColorOptions: new PassiveColorOptions(Color.cyan, Color.cyan)),
        //        new PassiveOptions(25, false, passiveColorOptions: new PassiveColorOptions(Color.cyan, Color.cyan)),
        //        new PassiveOptions(35, false),
        //        new PassiveOptions(38, false, passiveColorOptions: new PassiveColorOptions(Color.yellow, Color.yellow)),
        //        new PassiveOptions(40, false, passiveColorOptions: new PassiveColorOptions(Color.yellow, Color.yellow)),
        //        new PassiveOptions(58, false),
        //        new PassiveOptions(5, false),
        //        new PassiveOptions(28, false),
        //        new PassiveOptions(57, bannedEmotionCardSelection: true, bannedEgoFloorCards: true),
        //        new PassiveOptions(37,
        //            canBeUsedWithPassivesOne: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 8) },
        //            passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
        //        new PassiveOptions(17, passiveColorOptions: new PassiveColorOptions(Color.white, Color.white),
        //            canBeUsedWithPassivesOne: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 12) }),
        //        new PassiveOptions(26,
        //            canBeUsedWithPassivesOne: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 20) }),
        //        new PassiveOptions(61, canBeUsedWithPassivesOne: new List<LorId>
        //        {
        //            new LorId(KamiyoModParameters.PackageId, 6), new LorId(KamiyoModParameters.PackageId, 8),
        //            new LorId(KamiyoModParameters.PackageId, 12),
        //            new LorId(KamiyoModParameters.PackageId, 20), new LorId(KamiyoModParameters.PackageId, 24),
        //            new LorId(KamiyoModParameters.PackageId, 40),
        //            new LorId(KamiyoModParameters.SephirahBundlePackageId, 27)
        //        }, passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
        //        new PassiveOptions(21, cannotBeUsedWithPassives: new List<LorId> { new LorId(250115) }),
        //        new PassiveOptions(37, passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
        //        new PassiveOptions(22,
        //            cannotBeUsedWithPassives: new List<LorId>
        //            {
        //                new LorId(KamiyoModParameters.MaryModPackageId, 3),
        //                new LorId(KamiyoModParameters.VortexModPackageId, 3),
        //                new LorId(KamiyoModParameters.VortexModPackageId, 8)
        //            }),
        //        new PassiveOptions(30, passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
        //        new PassiveOptions(14, forceAggroOptions: new ForceAggroOptions(forceAggroLastDie: true)),
        //        new PassiveOptions(31),
        //        new PassiveOptions(32, bannedEmotionCardSelection: true,
        //            cannotBeUsedWithPassives: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 51) },
        //            passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
        //        new PassiveOptions(33, bannedEgoFloorCards: true,
        //            cannotBeUsedWithPassives: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 51) },
        //            passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
        //        new PassiveOptions(51, bannedEmotionCardSelection: true, bannedEgoFloorCards: true,
        //            cannotBeUsedWithPassives: new List<LorId>
        //                { new LorId(KamiyoModParameters.PackageId, 32), new LorId(KamiyoModParameters.PackageId, 33) },
        //            passiveColorOptions: new PassiveColorOptions(Color.red, Color.red))
        //    });
        //}

        //private static void OnInitDropBooks()
        //{
        //    ModParameters.DropBookOptions.Add(KamiyoModParameters.PackageId, new List<DropBookOptions>
        //    {
        //        new DropBookOptions(3, new DropBookColorOptions(Color.white, Color.white)),
        //        new DropBookOptions(4, new DropBookColorOptions(Color.gray, Color.gray)),
        //        new DropBookOptions(5,
        //            new DropBookColorOptions(Color.gray, Color.gray)),
        //        new DropBookOptions(7, new DropBookColorOptions(Color.cyan, Color.cyan)),
        //        new DropBookOptions(9, new DropBookColorOptions(Color.yellow, Color.yellow))
        //    });
        //}
    }
}