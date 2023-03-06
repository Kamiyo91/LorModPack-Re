using System.Collections.Generic;
using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Enum;
using BigDLL4221.Models;
using KamiyoModPack.Hayate_Re21341;
using KamiyoModPack.Hayate_Re21341.Buffs;
using KamiyoModPack.Hayate_Re21341.MechUtil;
using KamiyoModPack.Kamiyo_Re21341.Buffs;
using KamiyoModPack.Kamiyo_Re21341.MapManager;
using KamiyoModPack.Kamiyo_Re21341.MechUtil;
using KamiyoModPack.Kamiyo_Re21341.Passives;
using KamiyoModPack.Mio_Re21341;
using KamiyoModPack.Mio_Re21341.Buffs;
using KamiyoModPack.OldSamurai_Re21341.MapManager;
using KamiyoModPack.Raziel_Re21341;
using KamiyoModPack.Raziel_Re21341.Buffs;
using KamiyoModPack.Raziel_Re21341.MechUtil;
using KamiyoModPack.Util_Re21341.CommonBuffs;
using KamiyoModPack.Wilton_Re21341;
using KamiyoModPack.Wilton_Re21341.Buffs;
using LOR_XML;

namespace KamiyoModPack.BLL_Re21341.Models
{
    public static class KamiyoModParameters
    {
        public const string PackageId = "LorModPackRe21341.Mod";

        //public const string SephirahBundlePackageId = "SephirahBundleSe21341.Mod";
        //public const string MaryModPackageId = "MaryIb21341.Mod";
        //public const string VortexModPackageId = "VortexTowerModSa21341.Mod";
        public static string Path;
        public static readonly int EgoEmotionLevel = 3;

        // OldSamurai
        public static UnitModel SamuraiGhostNpc = new UnitModel(2, PackageId, 2, lockedEmotion: true);
        public static UnitModel SamuraiGhostPlayer = new UnitModel(10000002, PackageId, 2, lockedEmotion: true);

        public static UnitModel SamuraiGhostPlayerEmotion = new UnitModel(10000009, PackageId, 2, lockedEmotion: true,
            summonedOnPlay: true, autoPlay: true);

        public static MapModel SamuraiMapNpc = new MapModel(typeof(OldSamurai_Re21341MapManager), "OldSamurai_Re21341",
            oneTurnEgo: false, bgy: 0.2f, originalMapStageIds: new List<LorId> { new LorId(PackageId, 1) });

        public static MapModel SamuraiMapPlayer = new MapModel(typeof(OldSamuraiPlayer_Re21341MapManager),
            "OldSamurai_Re21341", oneTurnEgo: false, bgy: 0.2f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 1) });

        //Mio
        public static UnitModel MioSupportUnit =
            new UnitModel(10000900, PackageId, 3, forcedEgoOnStart: true, additionalPassiveIds: new List<LorId>
            {
                new LorId(PackageId, 37), new LorId(PackageId, 57)
            }, additionalBuffs: new List<BattleUnitBuf> { new BattleUnitBuf_KurosawaEmblem_Re21341() });

        public static MapModel MioMap = new MapModel(typeof(Mio_Re21341MapManager), "Mio_Re21341", bgy: 0.2f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 2), new LorId(PackageId, 9) });

        //Kamiyo
        public static UnitModel MioMemoryUnit = new UnitModel(5, PackageId, 5);

        public static MapModel KamiyoMap1 = new MapModel(typeof(Kamiyo1_Re21341MapManager), "Kamiyo1_Re21341",
            bgy: 0.2f, fy: 0.45f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 3) });

        public static MapModel KamiyoMap2 = new MapModel(typeof(Kamiyo2_Re21341MapManager), "Kamiyo2_Re21341",
            bgy: 0.475f, fy: 0.225f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 3) });

        //Hayate
        public static MapModel HayateMap = new MapModel(typeof(Hayate_Re21341MapManager), "Hayate_Re21341", bgy: 0.3f,
            fy: 0.475f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 4), new LorId(PackageId, 10) });

        //public static MapModel HayateSephirahMap = new MapModel(typeof(HayateSephirah_Re21341MapManager),
        //    "Hayate_Re21341", bgy: 0.3f,
        //    fy: 0.475f,
        //    originalMapStageIds: new List<LorId> { new LorId(PackageId, 4), new LorId(PackageId, 10) });

        public static UnitModel KamiyoSoloUnit = new UnitModel(10000901, PackageId, 4);

        //Wilton
        public static UnitModel HayateLastScene = new UnitModel(6, PackageId, 6);
        public static UnitModel WillWisp = new UnitModel(9, PackageId, 9);

        public static MapModel WiltonMap = new MapModel(typeof(Wilton_Re21341MapManager), "Wilton_Re21341", bgy: 0.2f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 6), new LorId(PackageId, 11) });

        //Raziel
        public static MapModel RazielMap = new MapModel(typeof(Raziel_Re21341MapManager), "Raziel_Re21341", bgy: 0.375f,
            fy: 0.225f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 7) });

        //PreBattleUnits
        //public static UnitModel KamiyoPreBattleModel = new UnitModel(10000901, PackageId, 4,
        //    skinName: "KamiyoNormal_Re21341", additionalPassiveIds: new List<LorId> { new LorId(PackageId, 17) });

        //public static UnitModel MioPreBattleModel = new UnitModel(10000900, PackageId, 3,
        //    skinName: "MioNormalEye_Re21341", additionalPassiveIds: new List<LorId> { new LorId(PackageId, 37) });

        //public static List<int> EmotionCardIds = new List<int>
        //    { 21341, 21342, 21343, 21344, 21345, 21346, 21347, 21348, 21349, 21350, 21351, 21352, 21353, 21354, 21355 };

        //public static List<int> FloorEgoCardIds = new List<int>
        //    { 21356, 21357, 21358, 21359, 21360 };

        //public static string PoolCode = "Kurosawa_21341";
    }

    public class OldSamuraiUtil
    {
        public NpcMechUtilBase OldSamuraiNpcUtil = new NpcMechUtilBase(new NpcMechUtilBaseModel(
            "PhaseOldSamuraiRe21341", mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                { 0, new MechPhaseOptions(2, mechOnDeath: true, hasCustomMap: true) },
                {
                    1,
                    new MechPhaseOptions(2, hpRecoverOnChangePhase: 999, hasCustomMap: true,
                        unitsThatDieTogetherByPassive: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 2) },
                        musicOptions: new MusicOptions("Hornet_Re21341.ogg", "OldSamurai_Re21341"),
                        summonUnit: new List<UnitModel>
                        {
                            KamiyoModParameters.SamuraiGhostNpc, KamiyoModParameters.SamuraiGhostNpc,
                            KamiyoModParameters.SamuraiGhostNpc
                        })
                }
            }), KamiyoModParameters.PackageId);

        public MechUtilBase OldSamuraiPlayerUtil = new MechUtilBase(new MechUtilBaseModel(
            egoMaps: new Dictionary<LorId, MapModel>
                { { new LorId(KamiyoModParameters.PackageId, 999999), KamiyoModParameters.SamuraiMapPlayer } },
            personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(KamiyoModParameters.PackageId, 8), new PersonalCardOptions(activeEgoCard: true) }
            }, egoOptions:
            new Dictionary<int, EgoOptions>
            {
                {
                    0,
                    new EgoOptions(assimilationEgoWithMap: new LorId(KamiyoModParameters.PackageId, 999999),
                        summonUnitCustomData: new List<UnitModel>
                        {
                            KamiyoModParameters.SamuraiGhostPlayer, KamiyoModParameters.SamuraiGhostPlayer,
                            KamiyoModParameters.SamuraiGhostPlayer
                        },
                        unitsThatDieTogetherByPassive: new List<LorId>
                            { new LorId(KamiyoModParameters.PackageId, 5) }, removeEgoWhenSolo: true)
                }
            }), KamiyoModParameters.PackageId);
    }

    public class MioUtil
    {
        public NpcMechUtilBase MioNpcUtil = new NpcMechUtilBase(new NpcMechUtilBaseModel("PhaseMioRe21341",
            egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 900), KamiyoModParameters.MioMap }
            }, originalSkinName: "MioNormalEye_Re21341", egoOptions:
            new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_CorruptedGodAuraRelease_Re21341(), "MioRedEye_Re21341", true,
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "MioEnemy",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("MioEnemyEgoActive1_Re21341")).Value.Desc
                            }
                        })
                }
            }, mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                { 0, new MechPhaseOptions(2, 271, hasCustomMap: true, alwaysAimSlowestTargetDie: true) },
                {
                    1, new MechPhaseOptions(2, loweredCost: 2, setEmotionLevel: 4, changeCardCost: true,
                        startMassAttack: true, forceEgo: true, setCounterToMax: true, alwaysAimSlowestTargetDie: true,
                        hasCustomMap: true, musicOptions: new MusicOptions("MioPhase2_Re21341.ogg", "Mio_Re21341"),
                        creatureFilter: true,
                        summonPlayerUnit: new List<UnitModel> { KamiyoModParameters.MioSupportUnit },
                        egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                        {
                            new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 900))
                        }, soundEffectPath: new List<string> { "Creature/Angry_Meet" })
                }
            }, survive: true, recoverToHp: 179, reloadMassAttackOnLethal: true, recoverLightOnSurvive: true,
            specialCardOptions: new SpecialCardOption(2, typeof(BattleUnitBuf_SakuraPetal_Re21341),
                keywordBuffs: new Dictionary<KeywordBuf, int>
                {
                    { KeywordBuf.Strength, 1 }, { KeywordBuf.Endurance, 1 }
                }), surviveAbDialogList: new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "MioEnemy",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("MioEnemySurvive1_Re21341"))
                        .Value.Desc
                }
            }), KamiyoModParameters.PackageId);

        public MechUtil_Mio MioPlayerUtil = new MechUtil_Mio(new MechUtilBaseModel(
            egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 10), KamiyoModParameters.MioMap }
            }, firstEgoFormCard: new LorId(KamiyoModParameters.PackageId, 9), recoverToHp: 65, survive: true,
            originalSkinName: "MioNormalEye_Re21341", surviveAbDialogList: new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "Mio",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("MioSurvive1_Re21341"))
                        .Value.Desc
                },
                new AbnormalityCardDialog
                {
                    id = "Mio",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("MioSurvive2_Re21341"))
                        .Value.Desc
                }
            }, egoOptions: new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_GodAuraRelease_Re21341(), "MioRedEye_Re21341", true,
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "Mio",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("MioEgoActive1_Re21341"))
                                    .Value.Desc
                            },
                            new AbnormalityCardDialog
                            {
                                id = "Mio",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("MioEgoActive2_Re21341"))
                                    .Value.Desc
                            }
                        }, egoAbColorColor: AbColorType.Positive)
                }
            }, personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(KamiyoModParameters.PackageId, 9), new PersonalCardOptions(true, activeEgoCard: true) },
                { new LorId(KamiyoModParameters.PackageId, 10), new PersonalCardOptions(true) }
            }));
    }

    public class KamiyoUtil
    {
        public NpcMechUtil_Kamiyo KamiyoNpcUtil = new NpcMechUtil_Kamiyo(new NpcMechUtilBaseModel("PhaseKamiyoRe21341",
            permanentBuffList: new List<PermanentBuffOptions>
            {
                new PermanentBuffOptions(new BattleUnitBuf_Shock_Re21341())
            },
            egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 902), KamiyoModParameters.KamiyoMap2 }
            }, originalSkinName: "KamiyoNormal_Re21341", egoOptions:
            new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_AlterEgoRelease_Re21341(), "KamiyoMask_Re21341", true,
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "KamiyoEnemy",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("KamiyoEnemyEgoActive1_Re21341")).Value.Desc
                            }
                        })
                }
            }, mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                { 0, new MechPhaseOptions(2, 161, hasCustomMap: true) },
                {
                    1, new MechPhaseOptions(2, extraMaxHp: 314, setEmotionLevel: 4, hpRecoverOnChangePhase: 9999,
                        loweredCost: 2, changeCardCost: true, extraMaxStagger: 173, hasCustomMap: true,
                        startMassAttack: true, setCounterToMax: true, mapOrderIndex: 1,
                        additionalPassiveByIds: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 11) },
                        summonUnit: new List<UnitModel> { KamiyoModParameters.MioMemoryUnit }, forceEgo: true,
                        egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                        {
                            new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 902))
                        },
                        unitsThatDieTogetherByPassive: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 5) })
                }
            }, nearDeathBuffType: new BattleUnitBuf_NearDeathNpc_Re21341(), survive: true, recoverToHp: 161,
            reloadMassAttackOnLethal: true, recoverLightOnSurvive: true, specialCardOptions: new SpecialCardOption(2,
                keywordBuffs: new Dictionary<KeywordBuf, int>
                {
                    { KeywordBuf.Strength, 1 }, { KeywordBuf.Endurance, 1 }, { KeywordBuf.Burn, 3 }
                }), surviveAbDialogList: new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "KamiyoEnemy",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("KamiyoEnemySurvive1_Re21341")).Value.Desc
                }
            }));

        public MechUtil_Kamiyo KamiyoPlayerUtil = new MechUtil_Kamiyo(new MechUtilBaseModel(
            permanentBuffList: new List<PermanentBuffOptions>
            {
                new PermanentBuffOptions(new BattleUnitBuf_Shock_Re21341())
            },
            egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 16), KamiyoModParameters.KamiyoMap2 }
            }, firstEgoFormCard: new LorId(KamiyoModParameters.PackageId, 17), recoverToHp: 64,
            nearDeathBuffType: new BattleUnitBuf_NearDeath_Re21341(), survive: true,
            originalSkinName: "KamiyoNormal_Re21341", surviveAbDialogList: new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "Kamiyo",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("KamiyoSurvive1_Re21341"))
                        .Value.Desc
                },
                new AbnormalityCardDialog
                {
                    id = "Kamiyo",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("KamiyoSurvive2_Re21341"))
                        .Value.Desc
                }
            }, egoOptions: new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_AlterEgoRelease_Re21341(), "KamiyoMask_Re21341", true,
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "Kamiyo",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive1_Re21341"))
                                    .Value.Desc
                            },
                            new AbnormalityCardDialog
                            {
                                id = "Kamiyo",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive2_Re21341"))
                                    .Value.Desc
                            },
                            new AbnormalityCardDialog
                            {
                                id = "Kamiyo",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("KamiyoEgoActive3_Re21341"))
                                    .Value.Desc
                            }
                        }, additionalPassiveIds: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 14) })
                }
            }, personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(KamiyoModParameters.PackageId, 17), new PersonalCardOptions(true, activeEgoCard: true) },
                { new LorId(KamiyoModParameters.PackageId, 16), new PersonalCardOptions(true) },
                { new LorId(KamiyoModParameters.PackageId, 60), new PersonalCardOptions(true, true) }
            }));

        public SummonedUnitStatModelLinked MioMemoryUtil = new SummonedUnitStatModelLinked(
            new PassiveAbility_AlterEgoNpc_Re21341(), 161, reviveAfterScenesNpc: 0, hpRecoveredWithRevive: 999,
            maxCounter: 4, additionalSpeedDie: 2, useCustomData: false, massAttackCards: new List<LorId>
            {
                new LorId(KamiyoModParameters.PackageId, 900)
            }, originalSkinName: "MioNormalEye_Re21341", maxCardCost: 4, loweredCardCost: 2,
            egoOptions: new EgoOptions(new BattleUnitBuf_GodAuraRelease_Re21341(), "MioRedEye_Re21341",
                activeEgoOnStart: true));
    }

    public class RazielUtil
    {
        public NpcMechUtil_Raziel RazielNpcUtil = new NpcMechUtil_Raziel(new NpcMechUtilBaseModel("PhaseRazielRe21341",
            egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 906), KamiyoModParameters.RazielMap }
            }, mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                {
                    0, new MechPhaseOptions(mechOnScenesCount: true, hasCustomMap: true, scenesBeforeNextPhase: 2,
                        speedDieAdder: 2, startMassAttack: true, maxCounter: 0,
                        egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                        {
                            new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 906))
                        })
                },
                {
                    1, new MechPhaseOptions(mechOnScenesCount: true, hasCustomMap: true, scenesBeforeNextPhase: 4,
                        speedDieAdder: 2, startMassAttack: true, additionalPassiveByIds: new List<LorId>
                        {
                            new LorId(KamiyoModParameters.PackageId, 41)
                        }, loweredCost: 2, changeCardCost: true, forceEgo: true,
                        musicOptions: new MusicOptions("RazielPhase2_Re21341.ogg", "Raziel_Re21341"),
                        egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                        {
                            new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 906))
                        })
                },
                { 2, new MechPhaseOptions(hasExtraFunctionRoundStart: true) }
            }, egoOptions: new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_OwlSpirit_Re21341(),
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "RazielEnemy",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("RazielEnemyEgoActive1_Re21341")).Value.Desc
                            }
                        })
                }
            }));

        public MechUtilBase RazielPlayerUtil = new MechUtilBase(new MechUtilBaseModel(forceRetreatOnRevive: true,
            reviveOnDeath: true, recoverHpOnRevive: 999, personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(KamiyoModParameters.PackageId, 57), new PersonalCardOptions(true, activeEgoCard: true) },
                { new LorId(KamiyoModParameters.PackageId, 58), new PersonalCardOptions() }
            }, firstEgoFormCard: new LorId(KamiyoModParameters.PackageId, 57),
            reviveAbDialogList: new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "RazielEnemy",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("RazielImmortal_Re21341")).Value.Desc
                }
            }, egoOptions: new Dictionary<int, EgoOptions>
            {
                { 0, new EgoOptions(new BattleUnitBuf_OwlSpirit_Re21341()) }
            }, egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 58), KamiyoModParameters.RazielMap }
            }), KamiyoModParameters.PackageId);
    }

    public class HayateUtil
    {
        public NpcMechUtil_Hayate HayateNpcUtil = new NpcMechUtil_Hayate(new NpcMechUtilBaseModel("PhaseHayateRe21341",
            new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_TrueGodAuraRelease_Re21341(),
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "HayateEnemy",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("HayateEnemyEgoActive1_Re21341")).Value.Desc
                            }
                        })
                }
            },
            permanentBuffList: new List<PermanentBuffOptions>
                { new PermanentBuffOptions(new BattleUnitBuf_EntertainMe_Re21341()) },
            mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                {
                    0,
                    new MechPhaseOptions(mechHp: 527, hasCustomMap: true, hasSpecialChangePhaseCondition: true,
                        speedDieAdder: 2, singletonBufMech: new SingletonBufMech(
                            new BattleUnitBuf_EntertainMe_Re21341(), 40,
                            new List<SpecialAttackCardOptions>
                                { new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 903), true) }))
                },
                {
                    1,
                    new MechPhaseOptions(mechHp: 100, hasCustomMap: true, speedDieAdder: 4, loweredCost: 2,
                        additionalPassiveByIds: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 44) },
                        changeCardCost: true, creatureFilter: true,
                        summonOriginalUnitByIndex: new List<int> { 1, 2, 3 },
                        musicOptions: new MusicOptions("HayatePhase2_Re21341.ogg", "Hayate_Re21341"), forceEgo: true,
                        singletonBufMech: new SingletonBufMech(new BattleUnitBuf_EntertainMe_Re21341(), 40,
                            new List<SpecialAttackCardOptions>
                                { new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 903), true) },
                            true))
                },
                {
                    2,
                    new MechPhaseOptions(mechOnScenesCount: true, creatureFilter: true, hasCustomMap: true,
                        speedDieAdder: 5, loweredCost: 5,
                        changeCardCost: true, maxCost: 6, scenesBeforeNextPhase: 0,
                        singletonBufMech: new SingletonBufMech(new BattleUnitBuf_EntertainMe_Re21341(), 0,
                            new List<SpecialAttackCardOptions>
                                { new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 904)) }))
                },
                {
                    3,
                    new MechPhaseOptions(hasExtraFunctionRoundPreEnd: true, creatureFilter: true, hasCustomMap: true,
                        onPhaseChangeDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "Hayate",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("HayateEnemyFinalPhase1_Re21341")).Value.Desc
                            }
                        },
                        musicOptions: new MusicOptions("HayatePhase3_Re21341.ogg", "Hayate_Re21341"),
                        removebuffs: new List<BattleUnitBuf> { new BattleUnitBuf_EntertainMe_Re21341() },
                        buffOptions: new MechBuffOptions(new List<BattleUnitBuf>
                            { new BattleUnitBuf_EntertainMeFinalPhase_Re21341() }),
                        speedDieAdder: 4)
                }
            }));

        public MechUtil_Hayate HayatePlayerUtil = new MechUtil_Hayate(new MechUtilBaseModel(
            permanentBuffList: new List<PermanentBuffOptions>
                { new PermanentBuffOptions(new BattleUnitBuf_EntertainMe_Re21341()) }, survive: true,
            recoverToHp: 75, personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(KamiyoModParameters.PackageId, 28), new PersonalCardOptions(true, activeEgoCard: true) },
                { new LorId(KamiyoModParameters.PackageId, 29), new PersonalCardOptions(true, expireAfterUse: true) },
                {
                    new LorId(KamiyoModParameters.PackageId, 907),
                    new PersonalCardOptions(true, expireAfterUse: true, egoPhase: 99)
                }
            }, firstEgoFormCard: new LorId(KamiyoModParameters.PackageId, 28),
            egoOptions: new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_TrueGodAuraRelease_Re21341(), activeEgoOnSurvive: true,
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "Hayate",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("HayateEgoActive1_Re21341"))
                                    .Value.Desc
                            },
                            new AbnormalityCardDialog
                            {
                                id = "Hayate",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("HayateEgoActive2_Re21341"))
                                    .Value.Desc
                            }
                        }, egoAbColorColor: AbColorType.Positive)
                }
            }));
    }

    public class WiltonUtil
    {
        public NpcMechUtilBase WiltonNpcUtil = new NpcMechUtilBase(new NpcMechUtilBaseModel("PhaseWiltonRe21341",
            survive: true, recoverToHp: 61, reloadMassAttackOnLethal: true, recoverLightOnSurvive: true,
            addBuffsOnPlayerUnitsAtStart: new List<BattleUnitBuf> { new BattleUnitBuf_Vip_Re21341() },
            mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                { 0, new MechPhaseOptions(2, 271) },
                {
                    1,
                    new MechPhaseOptions(2, loweredCost: 2, changeCardCost: true, forceEgo: true, startMassAttack: true,
                        setCounterToMax: true, maxCounter: 5,
                        additionalPassiveByIds: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 36) },
                        unitsThatDieTogetherByPassive: new List<LorId> { new LorId(KamiyoModParameters.PackageId, 36) },
                        egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                            { new SpecialAttackCardOptions(new LorId(KamiyoModParameters.PackageId, 905)) },
                        summonUnit: new List<UnitModel>
                        {
                            KamiyoModParameters.WillWisp, KamiyoModParameters.WillWisp, KamiyoModParameters.WillWisp
                        })
                }
            }, egoOptions: new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_Vengeance_Re21341(),
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "WiltonEnemy",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("WiltonEnemyEgoActive1_Re21341")).Value.Desc
                            }
                        })
                }
            }, surviveAbDialogList: new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "WiltonEnemy",
                    dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Equals("WiltonEnemySurvive1_Re21341"))
                        .Value.Desc
                }
            }, egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 905), KamiyoModParameters.WiltonMap }
            }), KamiyoModParameters.PackageId);

        public MechUtilBase WiltonPlayerUtil = new MechUtilBase(new MechUtilBaseModel(
            personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(KamiyoModParameters.PackageId, 47), new PersonalCardOptions(true, activeEgoCard: true) },
                { new LorId(KamiyoModParameters.PackageId, 48), new PersonalCardOptions(true) },
                { new LorId(KamiyoModParameters.PackageId, 30), new PersonalCardOptions(true, true) }
            }, firstEgoFormCard: new LorId(KamiyoModParameters.PackageId, 47), egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(KamiyoModParameters.PackageId, 48), KamiyoModParameters.WiltonMap }
            },
            egoOptions: new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_Vengeance_Re21341(),
                        egoAbDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "Wilton",
                                dialog = ModParameters.LocalizedItems[KamiyoModParameters.PackageId].EffectTexts
                                    .FirstOrDefault(x => x.Key.Equals("WiltonEgoActive1_Re21341")).Value.Desc
                            }
                        })
                }
            }), KamiyoModParameters.PackageId);
    }
}