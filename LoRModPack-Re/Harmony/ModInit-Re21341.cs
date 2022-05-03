using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BLL_Re21341.Models;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using MonoMod.Utils;

namespace LoRModPack_Re21341.Harmony
{
    public class ModInit_Re21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            InitParameters();
            MapStaticUtil.GetArtWorks(new DirectoryInfo(KamiyoModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance, KamiyoModParameters.PackageId);
            UnitUtil.ChangePassiveItem(KamiyoModParameters.PackageId);
            SkinUtil.LoadBookSkinsExtra(KamiyoModParameters.PackageId);
            SkinUtil.PreLoadBufIcons();
            LocalizeUtil.AddLocalLocalize(KamiyoModParameters.Path, KamiyoModParameters.PackageId);
            LocalizeUtil.RemoveError();
        }

        private static void InitParameters()
        {
            ModParameters.PackageIds.Add(KamiyoModParameters.PackageId);
            KamiyoModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Path.Add(KamiyoModParameters.Path);
            ModParameters.DefaultKeyword.Add(KamiyoModParameters.PackageId, "LoRModPage_Re21341");
            ModParameters.OnlyCardKeywords.AddRange(new List<Tuple<List<string>, List<LorId>, LorId>>
            {
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "KamiyoPage_Re21341" },
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 19), new LorId(KamiyoModParameters.PackageId, 20),
                        new LorId(KamiyoModParameters.PackageId, 21), new LorId(KamiyoModParameters.PackageId, 22)
                    }, new LorId(KamiyoModParameters.PackageId, 10000004)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "MioPage_Re21341" },
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 12), new LorId(KamiyoModParameters.PackageId, 13),
                        new LorId(KamiyoModParameters.PackageId, 14), new LorId(KamiyoModParameters.PackageId, 15)
                    }, new LorId(KamiyoModParameters.PackageId, 10000003)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "HayatePage_Re21341" },
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 23), new LorId(KamiyoModParameters.PackageId, 24),
                        new LorId(KamiyoModParameters.PackageId, 25), new LorId(KamiyoModParameters.PackageId, 26),
                        new LorId(KamiyoModParameters.PackageId, 27)
                    }, new LorId(KamiyoModParameters.PackageId, 10000005)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "SamuraiPage_Re21341" },
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 3), new LorId(KamiyoModParameters.PackageId, 4),
                        new LorId(KamiyoModParameters.PackageId, 5), new LorId(KamiyoModParameters.PackageId, 6),
                        new LorId(KamiyoModParameters.PackageId, 7)
                    }, new LorId(KamiyoModParameters.PackageId, 10000001)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "WiltonPage_Re21341" },
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 43), new LorId(KamiyoModParameters.PackageId, 44),
                        new LorId(KamiyoModParameters.PackageId, 45), new LorId(KamiyoModParameters.PackageId, 46)
                    }, new LorId(KamiyoModParameters.PackageId, 10000006)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "RazielPage_Re21341" },
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 52), new LorId(KamiyoModParameters.PackageId, 53),
                        new LorId(KamiyoModParameters.PackageId, 54), new LorId(KamiyoModParameters.PackageId, 55)
                    }, new LorId(KamiyoModParameters.PackageId, 10000007))
            });
            ModParameters.DynamicNames.AddRange(new Dictionary<LorId, LorId>
            {
                { new LorId(KamiyoModParameters.PackageId, 10000003), new LorId(KamiyoModParameters.PackageId, 3) },
                { new LorId(KamiyoModParameters.PackageId, 10000004), new LorId(KamiyoModParameters.PackageId, 4) },
                { new LorId(KamiyoModParameters.PackageId, 10000005), new LorId(KamiyoModParameters.PackageId, 6) },
                { new LorId(KamiyoModParameters.PackageId, 10000006), new LorId(KamiyoModParameters.PackageId, 8) },
                { new LorId(KamiyoModParameters.PackageId, 10000007), new LorId(KamiyoModParameters.PackageId, 10) }
            });
            ModParameters.BooksIds.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 10000001), new LorId(KamiyoModParameters.PackageId, 10000003),
                new LorId(KamiyoModParameters.PackageId, 10000004), new LorId(KamiyoModParameters.PackageId, 10000005),
                new LorId(KamiyoModParameters.PackageId, 10000006), new LorId(KamiyoModParameters.PackageId, 10000007)
            });
            ModParameters.PersonalCardList.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 1), new LorId(KamiyoModParameters.PackageId, 9),
                new LorId(KamiyoModParameters.PackageId, 17), new LorId(KamiyoModParameters.PackageId, 28),
                new LorId(KamiyoModParameters.PackageId, 29), new LorId(KamiyoModParameters.PackageId, 42),
                new LorId(KamiyoModParameters.PackageId, 47), new LorId(KamiyoModParameters.PackageId, 57),
                new LorId(KamiyoModParameters.PackageId, 59), new LorId(KamiyoModParameters.PackageId, 60),
                new LorId(KamiyoModParameters.PackageId, 61), new LorId(KamiyoModParameters.PackageId, 907)
            });
            ModParameters.EgoPersonalCardList.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 8), new LorId(KamiyoModParameters.PackageId, 10),
                new LorId(KamiyoModParameters.PackageId, 16), new LorId(KamiyoModParameters.PackageId, 48),
                new LorId(KamiyoModParameters.PackageId, 58), new LorId(KamiyoModParameters.PackageId, 901)
            });
            ModParameters.UntransferablePassives.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 6), new LorId(KamiyoModParameters.PackageId, 8),
                new LorId(KamiyoModParameters.PackageId, 12), new LorId(KamiyoModParameters.PackageId, 20),
                new LorId(KamiyoModParameters.PackageId, 24), new LorId(KamiyoModParameters.PackageId, 35),
                new LorId(KamiyoModParameters.PackageId, 40), new LorId(KamiyoModParameters.PackageId, 58)
            });
            ModParameters.SkinNameIds.AddRange(new List<Tuple<string, List<LorId>, string>>
            {
                new Tuple<string, List<LorId>, string>("KamiyoMask_Re21341",
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 10000004), new LorId(KamiyoModParameters.PackageId, 4)
                    },
                    "KamiyoNormal_Re21341"),
                new Tuple<string, List<LorId>, string>("MioRedEye_Re21341",
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 10000003), new LorId(KamiyoModParameters.PackageId, 3)
                    },
                    "MioNormalEye_Re21341")
            });
            ModParameters.SpritePreviewChange.AddRange(new Dictionary<string, List<LorId>>
            {
                { "MioDefault_Re21341", new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000003) } },
                { "KamiyoDefault_Re21341", new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000004) } },
                { "HayateDefault_Re21341", new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000005) } },
                { "WiltonDefault_Re21341", new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000006) } },
                { "RazielDefault_Re21341", new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000007) } },
                {
                    "FragmentDefault_Re21341",
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 10000008),
                        new LorId(KamiyoModParameters.PackageId, 10000011),
                        new LorId(KamiyoModParameters.PackageId, 10000012)
                    }
                },
                {
                    "FragmentBlueDefault_Re21341",
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 10000013),
                        new LorId(KamiyoModParameters.PackageId, 10000014),
                        new LorId(KamiyoModParameters.PackageId, 10000015)
                    }
                },
                { "FragmentRedDefault_Re21341", new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000016) } }
            });
            ModParameters.NoEgoFloorUnit.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 10000900), new LorId(KamiyoModParameters.PackageId, 10000002)
            });
            ModParameters.EmotionExcludePassive.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 57)
            });
            ModParameters.SupportCharPassive.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 57)
            });
            ModParameters.DefaultSpritePreviewChange.AddRange(new Dictionary<string, List<LorId>>
            {
                {
                    "Sprites/Books/Thumb/243003",
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 10000001),
                        new LorId(KamiyoModParameters.PackageId, 10000002)
                    }
                }
            });
            ModParameters.PreBattleUnits.AddRange(
                new List<Tuple<LorId, List<PreBattleUnitModel>, List<SephirahType>, PreBattleUnitSpecialCases>>
                {
                    new Tuple<LorId, List<PreBattleUnitModel>, List<SephirahType>, PreBattleUnitSpecialCases>(
                        new LorId(KamiyoModParameters.PackageId, 6), new List<PreBattleUnitModel>
                        {
                            new PreBattleUnitModel
                            {
                                UnitId = 10000901,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(KamiyoModParameters.PackageId, 4),
                                SkinName = "KamiyoNormal_Re21341",
                                PassiveIds = new List<LorId> { new LorId(KamiyoModParameters.PackageId, 17) }
                            },
                            new PreBattleUnitModel
                            {
                                UnitId = 10000900,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(KamiyoModParameters.PackageId, 3),
                                SkinName = "MioNormalEye_Re21341",
                                PassiveIds = new List<LorId> { new LorId(KamiyoModParameters.PackageId, 37) }
                            }
                        }, new List<SephirahType> { SephirahType.Keter }, PreBattleUnitSpecialCases.CustomUnits)
                });
            ModParameters.BannedEmotionStages.AddRange(new Dictionary<LorId, bool>
            {
                { new LorId(KamiyoModParameters.PackageId, 6), false },
                { new LorId(KamiyoModParameters.PackageId, 11), false }
            });
            ModParameters.SkinParameters.AddRange(new List<SkinNames>
            {
                new SkinNames
                {
                    PackageId = KamiyoModParameters.PackageId,
                    Name = "Wilton_Re21341",
                    SkinParameters = new List<SkinParameters>
                    {
                        new SkinParameters
                        {
                            PivotPosX = float.Parse("-28"), PivotPosY = float.Parse("-377"),
                            Motion = ActionDetail.Special, FileName = "Special.png"
                        },
                        new SkinParameters
                        {
                            PivotPosX = float.Parse("101"), PivotPosY = float.Parse("-326"),
                            Motion = ActionDetail.S1, FileName = "S1.png"
                        }
                    }
                }
            });
            ModParameters.OnlySephirahStage.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 1), new LorId(KamiyoModParameters.PackageId, 4),
                new LorId(KamiyoModParameters.PackageId, 10)
            });
            ModParameters.ExtraReward.AddRange(new Dictionary<LorId, ExtraRewards>
            {
                {
                    new LorId(KamiyoModParameters.PackageId, 12),
                    new ExtraRewards
                    {
                        MessageId = "KurosawaStoryDrop_Re21341",
                        DroppedKeypages = new List<LorId> { new LorId(KamiyoModParameters.PackageId, 10000017) }
                    }
                }
            });
            ModParameters.BannedEmotionSelectionUnit.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 2), new LorId(KamiyoModParameters.PackageId, 10000002),
                new LorId(KamiyoModParameters.PackageId, 10000901),
                new LorId(KamiyoModParameters.PackageId, 10000900)
            });
            ModParameters.UniquePassives.AddRange(new List<Tuple<LorId, List<LorId>>>
            {
                new Tuple<LorId, List<LorId>>(new LorId(KamiyoModParameters.PackageId, 37),
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 8) }),
                new Tuple<LorId, List<LorId>>(new LorId(KamiyoModParameters.PackageId, 17),
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 12) }),
                new Tuple<LorId, List<LorId>>(new LorId(KamiyoModParameters.PackageId, 26),
                    new List<LorId> { new LorId(KamiyoModParameters.PackageId, 20) }),
                new Tuple<LorId, List<LorId>>(new LorId(KamiyoModParameters.PackageId, 61),
                    new List<LorId>
                    {
                        new LorId(KamiyoModParameters.PackageId, 6), new LorId(KamiyoModParameters.PackageId, 8),
                        new LorId(KamiyoModParameters.PackageId, 12),
                        new LorId(KamiyoModParameters.PackageId, 20), new LorId(KamiyoModParameters.PackageId, 24),
                        new LorId(KamiyoModParameters.PackageId, 40),
                        new LorId("SephirahBundleSe21341.Mod", 27)
                    })
            });
            ModParameters.ExtraConditionPassives.AddRange(new List<Tuple<LorId, LorId>>
            {
                new Tuple<LorId, LorId>(new LorId(KamiyoModParameters.PackageId, 22),
                    new LorId("SaeModSa21341.Mod", 3)),
                new Tuple<LorId, LorId>(new LorId(KamiyoModParameters.PackageId, 21), new LorId(250115)),
                new Tuple<LorId, LorId>(new LorId(250115), new LorId(KamiyoModParameters.PackageId, 21))
            });
            ModParameters.ChainRelease.AddRange(new List<Tuple<LorId, LorId>>
            {
                new Tuple<LorId, LorId>(new LorId("SephirahBundleSe21341.Mod", 27),
                    new LorId(KamiyoModParameters.PackageId, 61))
            });
            ModParameters.ExtraMotions.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 10000004), new LorId(KamiyoModParameters.PackageId, 10000901),
                new LorId(KamiyoModParameters.PackageId, 4)
            });
            ModParameters.BookList.AddRange(new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId,6)
            });
        }
    }
}