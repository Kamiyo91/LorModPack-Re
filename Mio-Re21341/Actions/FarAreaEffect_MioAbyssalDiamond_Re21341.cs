﻿using System.Collections.Generic;
using System.Linq;
using Battle.DiceAttackEffect;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using LOR_DiceSystem;
using LOR_XML;
using Sound;
using UnityEngine;
using UtilLoader21341;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Actions
{
    public class FarAreaEffect_MioAbyssalDiamond_Re21341 : FarAreaEffect
    {
        private static int _motionCount;

        private Vector3 _dstPosAtkOneTarget;

        private float _elapsedAtkOneTarget;

        private float _elapsedEndAtk;

        private int _sign;

        private Vector3 _srcPosAtkOneTarget;

        private List<BattleFarAreaPlayManager.VictimInfo> _victimList;
        public override bool HasIndependentAction => true;

        public override void Init(BattleUnitModel self, params object[] args)
        {
            base.Init(self, args);
            _victimList = new List<BattleFarAreaPlayManager.VictimInfo>();
            _elapsedEndAtk = 0f;
            _elapsedAtkOneTarget = 0f;
            OnEffectStart();
            _self.view.charAppearance.ChangeMotion(ActionDetail.Default);
            var list = new List<BattleUnitModel> { self };
            SingletonBehavior<BattleCamManager>.Instance.FollowUnits(false, list);
            _sign = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
            _dstPosAtkOneTarget = Vector3.zero;
            _srcPosAtkOneTarget = Vector3.zero;
        }

        public override bool ActionPhase(float deltaTime, BattleUnitModel attacker,
            List<BattleFarAreaPlayManager.VictimInfo> victims,
            ref List<BattleFarAreaPlayManager.VictimInfo> defenseVictims)
        {
            var result = false;
            switch (state)
            {
                case EffectState.Start when !_self.moveDetail.isArrived:
                    return false;
                case EffectState.Start:
                    state = EffectState.GiveDamage;
                    _victimList = new List<BattleFarAreaPlayManager.VictimInfo>(victims);
                    break;
                case EffectState.GiveDamage:
                {
                    if (_elapsedAtkOneTarget < Mathf.Epsilon)
                    {
                        var ranged = attacker.currentDiceAction.card.GetSpec().Ranged;
                        if (ranged == CardRange.FarAreaEach)
                        {
                            var victimList = _victimList;
                            if (victimList != null && victimList.Count > 0)
                                if (_victimList.Exists(x => !x.unitModel.IsDead()))
                                {
                                    var list = _victimList.Where(victimInfo => !victimInfo.unitModel.IsDead()).ToList();
                                    var victimInfo2 = list[Random.Range(0, list.Count)];
                                    attacker.view.WorldPosition = victimInfo2.unitModel.view.WorldPosition;
                                    _motionCount = (_motionCount + 1) % 3;
                                    var detail = ActionDetail.Default;
                                    AudioClip audioClip = null;
                                    switch (_motionCount)
                                    {
                                        case 0:
                                        {
                                            audioClip = UnitUtil.GetSound(
                                                CustomMapHandler.GetCMU(KamiyoModParameters.PackageId),
                                                "Purple_Stab_Stab2", true);
                                            detail = ActionDetail.Penetrate;
                                            var componentType = ModParameters.CustomEffects["MioPierce_Re21341"];
                                            var diceAttackEffect =
                                                new GameObject("MioPierce_Re21341").AddComponent(componentType) as
                                                    DiceAttackEffect;
                                            if (diceAttackEffect == null) break;
                                            diceAttackEffect.Initialize(_self.view, victimInfo2.unitModel.view, 0.5f);
                                            diceAttackEffect.SetScale(1f);
                                            break;
                                        }
                                        case 1:
                                        {
                                            audioClip = UnitUtil.GetSound(
                                                CustomMapHandler.GetCMU(KamiyoModParameters.PackageId),
                                                "Purple_Slash_Hori", true);
                                            detail = ActionDetail.Slash;
                                            var componentType = ModParameters.CustomEffects["MioSlash_Re21341"];
                                            var diceAttackEffect =
                                                new GameObject("MioSlash_Re21341").AddComponent(componentType) as
                                                    DiceAttackEffect;
                                            if (diceAttackEffect == null) break;
                                            diceAttackEffect.Initialize(_self.view, victimInfo2.unitModel.view, 0.5f);
                                            diceAttackEffect.SetScale(1f);

                                            break;
                                        }
                                        case 2:
                                        {
                                            audioClip = UnitUtil.GetSound(
                                                CustomMapHandler.GetCMU(KamiyoModParameters.PackageId),
                                                "Purple_Slash_VertDown", true);
                                            detail = ActionDetail.Hit;
                                            var componentType = ModParameters.CustomEffects["MioHit_Re21341"];
                                            var diceAttackEffect =
                                                new GameObject("MioHit_Re21341").AddComponent(componentType) as
                                                    DiceAttackEffect;
                                            if (diceAttackEffect == null) break;
                                            diceAttackEffect.Initialize(_self.view, victimInfo2.unitModel.view, 0.5f);
                                            diceAttackEffect.SetScale(1f);
                                            break;
                                        }
                                    }

                                    attacker.view.charAppearance.ChangeMotion(detail);
                                    _sign = _sign == 1 ? -1 : 1;
                                    var b = new Vector3(
                                        SingletonBehavior<HexagonalMapManager>.Instance.tileSize * 4f *
                                        _self.view.transform.localScale.x / 1.5f, 0f, 0f) * _sign;
                                    _srcPosAtkOneTarget = victimInfo2.unitModel.view.WorldPosition + b;
                                    _dstPosAtkOneTarget = victimInfo2.unitModel.view.WorldPosition - b;
                                    attacker.view.WorldPosition = _srcPosAtkOneTarget;
                                    attacker.UpdateDirection(victimInfo2.unitModel.view.WorldPosition);
                                    var playingCard = victimInfo2.playingCard;
                                    if (playingCard?.currentBehavior != null)
                                    {
                                        if (attacker.currentDiceAction.currentBehavior.DiceResultValue >
                                            victimInfo2.playingCard.currentBehavior.DiceResultValue)
                                        {
                                            attacker.currentDiceAction.currentBehavior
                                                .GiveDamage(victimInfo2.unitModel);
                                            if (audioClip != null)
                                                SingletonBehavior<SoundEffectManager>.Instance.PlayClip(audioClip);
                                            if (victimInfo2.unitModel.IsDead())
                                            {
                                                var list2 = new List<BattleUnitModel>();
                                                list2.Add(_self);
                                                victimInfo2.unitModel.view.DisplayDlg(DialogType.DEATH, list2);
                                            }

                                            victimInfo2.unitModel.view.charAppearance
                                                .ChangeMotion(ActionDetail.Damaged);
                                            victimInfo2.destroyedDicesIndex.Add(victimInfo2.playingCard.currentBehavior
                                                .Index);
                                        }
                                        else
                                        {
                                            victimInfo2.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Guard);
                                            victimInfo2.unitModel.UpdateDirection(attacker.view.WorldPosition);
                                            if (!defenseVictims.Contains(victimInfo2)) defenseVictims.Add(victimInfo2);
                                        }
                                    }
                                    else
                                    {
                                        attacker.currentDiceAction.currentBehavior.GiveDamage(victimInfo2.unitModel);
                                        if (audioClip != null)
                                            SingletonBehavior<SoundEffectManager>.Instance.PlayClip(audioClip);
                                        if (victimInfo2.unitModel.IsDead())
                                        {
                                            var list3 = new List<BattleUnitModel>();
                                            list3.Add(_self);
                                            victimInfo2.unitModel.view.DisplayDlg(DialogType.DEATH, list3);
                                        }

                                        victimInfo2.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Damaged);
                                    }

                                    SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary
                                        .UpdateCharacterProfile(victimInfo2.unitModel, victimInfo2.unitModel.faction,
                                            victimInfo2.unitModel.hp, victimInfo2.unitModel.breakDetail.breakGauge);
                                    SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary
                                        .UpdateCharacterProfile(attacker, attacker.faction, attacker.hp,
                                            attacker.breakDetail.breakGauge);
                                    _victimList.Remove(victimInfo2);
                                }
                        }
                    }

                    _elapsedAtkOneTarget += deltaTime;
                    if (Vector3.SqrMagnitude(_dstPosAtkOneTarget - _srcPosAtkOneTarget) > Mathf.Epsilon)
                        attacker.view.WorldPosition = Vector3.Lerp(_srcPosAtkOneTarget, _dstPosAtkOneTarget,
                            _elapsedAtkOneTarget * 10f);

                    if (!(_elapsedAtkOneTarget > 0.25f)) return false;
                    _elapsedAtkOneTarget = 0f;
                    _srcPosAtkOneTarget = Vector3.zero;
                    _dstPosAtkOneTarget = Vector3.zero;
                    if (_victimList == null || _victimList.Count == 0)
                    {
                        state = EffectState.End;
                    }
                    else if (!_victimList.Exists(x => !x.unitModel.IsDead()))
                    {
                        _victimList.Clear();
                        state = EffectState.End;
                    }

                    break;
                }
                case EffectState.End:
                {
                    _elapsedEndAtk += deltaTime;
                    if (!(_elapsedEndAtk > 0.35f)) return false;
                    _self.view.charAppearance.ChangeMotion(ActionDetail.Default);
                    state = EffectState.None;
                    _elapsedEndAtk = 0f;
                    break;
                }
                default:
                {
                    if (_self.moveDetail.isArrived)
                    {
                        SingletonBehavior<BattleCamManager>.Instance.FollowUnits(false,
                            BattleObjectManager.instance.GetAliveList());
                        result = true;
                        Destroy(gameObject);
                    }

                    break;
                }
            }

            return result;
        }

        protected override void Update()
        {
            if (isRunning && _self.moveDetail.isArrived) isRunning = false;
        }

        private void OnDestroy()
        {
        }
    }
}