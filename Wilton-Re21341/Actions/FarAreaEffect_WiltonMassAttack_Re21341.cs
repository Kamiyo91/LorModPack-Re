using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LOR_XML;
using Sound;
using UnityEngine;

namespace KamiyoModPack.Wilton_Re21341.Actions
{
    public class FarAreaEffect_WiltonMassAttack_Re21341 : FarAreaEffect
    {
        private readonly float _additionalDst = 0;
        private readonly float _atkDelay = 0.6f;
        private readonly float _endDelay = 1.5f;
        private readonly float _knockbackSpeed = 1;
        private readonly float _moveSlowSpeed = 0.3f;
        private readonly float _moveSpeed = 5;
        private readonly float _startPortalOffsetX = 5;

        private List<GameObject> _createdPortals;

        private BattleFarAreaPlayManager.VictimInfo _currentVictim;

        private Vector3 _dstPosAtkOneTarget;

        private float _elapsedEndAction;

        private float _elapsedGiveDamage;

        private float _elapsedStart;

        private int _knockbackEnergy;

        private KingOfGreedMapManager _map;

        private Vector3 _srcPosAtkOneTarget;

        private GameObject _startPortal;

        private List<BattleFarAreaPlayManager.VictimInfo> _victimList;

        private CameraFilterPack_Blur_Radial filterRef;
        public override bool HasIndependentAction => true;

        public override void Init(BattleUnitModel self, params object[] args)
        {
            base.Init(self, args);
            state = EffectState.Start;
            var num = 1;
            if (self.direction == Direction.LEFT) num = -1;
            _elapsedStart = 0f;
            _elapsedGiveDamage = 0f;
            _elapsedEndAction = 0f;
            _dstPosAtkOneTarget = Vector3.zero;
            _srcPosAtkOneTarget = Vector3.zero;
            isRunning = false;
            _currentVictim = null;
            _knockbackEnergy = 0;
            Singleton<BattleFarAreaPlayManager>.Instance.SetActionDelay(0f);
            if (SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject as KingOfGreedMapManager)
                _map = SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject as KingOfGreedMapManager;
            else
                _map = null;
            if (_map != null && _map.specialAtkKing != null && _map.specialAtkKing.portalEffect != null)
                _startPortal = _map.specialAtkKing.portalEffect;
            else
                _startPortal = Util.LoadPrefab("Battle/CreatureEffect/5/KingOfGreed_Portal", transform);
            _startPortal.transform.position = self.view.WorldPosition + new Vector3(num * _startPortalOffsetX, 0f, 0f);
            foreach (var component in _startPortal.GetComponentsInChildren<ParticleSystem>())
            {
                var main = component.main;
                main.startColor = Color.red;
            }

            _createdPortals = new List<GameObject> { _startPortal };
            SingletonBehavior<BattleMapEffectManager>.Instance.AddEffectToList(gameObject);
        }

        public override bool ActionPhase(float deltaTime, BattleUnitModel attacker,
            List<BattleFarAreaPlayManager.VictimInfo> victims,
            ref List<BattleFarAreaPlayManager.VictimInfo> defenseVictims)
        {
            var result = false;
            switch (state)
            {
                case EffectState.Start:
                {
                    _elapsedStart += deltaTime;
                    if (!(_elapsedStart > 0.25f)) return result;
                    _self.view.WorldPosition = Vector3.Lerp(_self.view.WorldPosition, _startPortal.transform.position,
                        _elapsedStart * 2f);
                    if (!(_elapsedStart > 0.5f)) return result;
                    _elapsedStart = 0f;
                    state = EffectState.GiveDamage;
                    _victimList = new List<BattleFarAreaPlayManager.VictimInfo>(victims);
                    break;
                }
                case EffectState.GiveDamage:
                {
                    if (_elapsedGiveDamage < Mathf.Epsilon)
                    {
                        var victimList = _victimList;
                        if (victimList != null && victimList.Count > 0)
                            if (_victimList.Exists(x => !x.unitModel.IsDead()))
                            {
                                var list = _victimList.Where(victimInfo => !victimInfo.unitModel.IsDead()).ToList();
                                var v = list[Random.Range(0, list.Count)];
                                GameObject gameObject;
                                if (_map != null && _map.specialAtkTargets != null &&
                                    _map.specialAtkTargets.Exists(x => x.unit == v.unitModel))
                                {
                                    gameObject = _map.specialAtkTargets.Find(x => x.unit == v.unitModel).portalEffect;
                                }
                                else
                                {
                                    gameObject = Util.LoadPrefab("Battle/CreatureEffect/5/KingOfGreed_Portal",
                                        transform);
                                    var num = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
                                    var b = new Vector3(
                                        SingletonBehavior<HexagonalMapManager>.Instance.tileSize * 2f *
                                        _self.view.transform.localScale.x / 1.5f, 0f, 0f) * num;
                                    gameObject.transform.position = v.unitModel.view.WorldPosition + b;
                                    foreach (var component in gameObject.GetComponentsInChildren<ParticleSystem>())
                                    {
                                        var main = component.main;
                                        main.startColor = Color.red;
                                    }
                                }

                                _createdPortals.Add(gameObject);
                                attacker.view.WorldPosition = gameObject.transform.position;
                                attacker.UpdateDirection(v.unitModel.view.WorldPosition);
                                attacker.view.charAppearance.ChangeMotion(ActionDetail.Special);
                                _srcPosAtkOneTarget = gameObject.transform.position;
                                _dstPosAtkOneTarget = v.unitModel.view.WorldPosition +
                                                      (v.unitModel.view.WorldPosition - gameObject.transform.position)
                                                      .normalized * _additionalDst;
                                v.unitModel.UpdateDirection(attacker.view.WorldPosition);
                                _victimList.Remove(v);
                                var list2 = new List<BattleUnitModel>
                                {
                                    attacker,
                                    v.unitModel
                                };
                                SingletonBehavior<BattleCamManager>.Instance.FollowUnits(false, list2);
                                _currentVictim = v;
                            }
                    }

                    _elapsedGiveDamage += deltaTime;
                    if (_currentVictim != null && _elapsedGiveDamage > _atkDelay)
                    {
                        var playingCard = _currentVictim.playingCard;
                        if (playingCard?.currentBehavior != null)
                        {
                            if (attacker.currentDiceAction.currentBehavior.DiceResultValue >
                                _currentVictim.playingCard.currentBehavior.DiceResultValue)
                            {
                                attacker.currentDiceAction.currentBehavior.GiveDamage(_currentVictim.unitModel);
                                if (_currentVictim.unitModel.IsDead())
                                {
                                    var list3 = new List<BattleUnitModel> { _self };
                                    _currentVictim.unitModel.view.DisplayDlg(DialogType.DEATH, list3);
                                }

                                _currentVictim.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Damaged);
                                _currentVictim.destroyedDicesIndex.Add(_currentVictim.playingCard.currentBehavior
                                    .Index);
                                SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Creature/Greed_StrongAtk");
                                _knockbackEnergy = 15;
                            }
                            else
                            {
                                _currentVictim.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Guard);
                                _currentVictim.unitModel.UpdateDirection(attacker.view.WorldPosition);
                                if (!defenseVictims.Contains(_currentVictim)) defenseVictims.Add(_currentVictim);
                                SingletonBehavior<SoundEffectManager>.Instance.PlayClip(
                                    "Creature/Greed_StrongAtk_Defensed");
                                _knockbackEnergy = 5;
                            }
                        }
                        else
                        {
                            attacker.currentDiceAction.currentBehavior.GiveDamage(_currentVictim.unitModel);
                            if (_currentVictim.unitModel.IsDead())
                            {
                                var list4 = new List<BattleUnitModel> { _self };
                                _currentVictim.unitModel.view.DisplayDlg(DialogType.DEATH, list4);
                            }

                            _currentVictim.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Damaged);
                            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Creature/Greed_StrongAtk");
                            _knockbackEnergy = 15;
                        }

                        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(
                            _currentVictim.unitModel, _currentVictim.unitModel.faction, _currentVictim.unitModel.hp,
                            _currentVictim.unitModel.breakDetail.breakGauge);
                        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(
                            attacker, attacker.faction, attacker.hp, attacker.breakDetail.breakGauge);
                        _currentVictim.unitModel.moveDetail.Knockback(attacker, _knockbackEnergy, _knockbackSpeed, 0f);
                        _currentVictim = null;
                        attacker.view.charAppearance.ChangeMotion(ActionDetail.S1);
                        SetRadialFilter();
                    }

                    if (Vector3.SqrMagnitude(_dstPosAtkOneTarget - _srcPosAtkOneTarget) > Mathf.Epsilon)
                        attacker.view.WorldPosition = _elapsedGiveDamage > _atkDelay
                            ? Vector3.Lerp(_srcPosAtkOneTarget, _dstPosAtkOneTarget, _elapsedGiveDamage * _moveSpeed)
                            : Vector3.Lerp(_srcPosAtkOneTarget, _dstPosAtkOneTarget,
                                _elapsedGiveDamage * _moveSlowSpeed);
                    if (_elapsedGiveDamage > _endDelay)
                    {
                        _elapsedGiveDamage = 0f;
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
                    }

                    break;
                }
                case EffectState.End:
                {
                    _elapsedEndAction += deltaTime;
                    if (_elapsedEndAction > 0.35f)
                    {
                        _self.view.charAppearance.ChangeMotion(ActionDetail.Special);
                        state = EffectState.None;
                        _elapsedEndAction = 0f;
                    }

                    break;
                }
                default:
                {
                    if (_self.moveDetail.isArrived)
                    {
                        foreach (var gameObject2 in
                                 _createdPortals.Where(gameObject2 => gameObject2.gameObject != null))
                            Destroy(gameObject2);
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

        protected override void OnDisable()
        {
            base.OnDisable();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            DestroyEffects();
        }

        private void SetRadialFilter()
        {
            var instance = SingletonBehavior<BattleCamManager>.Instance;
            var camera = instance != null ? instance.EffectCam : null;
            if (camera == null) return;
            var cameraFilterPack_Blur_Radial = camera.gameObject.AddComponent<CameraFilterPack_Blur_Radial>();
            cameraFilterPack_Blur_Radial.StartCoroutine(RadialRoutine(cameraFilterPack_Blur_Radial));
            filterRef = cameraFilterPack_Blur_Radial;
            var instance2 = SingletonBehavior<BattleCamManager>.Instance;
            var autoScriptDestruct =
                (instance2 != null ? instance2.EffectCam.gameObject.AddComponent<AutoScriptDestruct>() : null) ?? null;
            if (autoScriptDestruct == null) return;
            autoScriptDestruct.targetScript = cameraFilterPack_Blur_Radial;
            autoScriptDestruct.time = 0.5f;
        }

        private static IEnumerator RadialRoutine(CameraFilterPack_Blur_Radial filter)
        {
            var e = 0f;
            const float intensity = 0.1f;
            while (e < 1f)
            {
                e += Time.deltaTime * 2f;
                filter.Intensity = Mathf.Lerp(intensity, 0f, e);
                yield return YieldCache.waitFrame;
            }
        }

        private void DestroyEffects()
        {
            if (filterRef != null)
            {
                Destroy(filterRef);
                filterRef = null;
            }

            if (_startPortal != null) Destroy(_startPortal);

            if (_createdPortals == null || _createdPortals.Count <= 0) return;
            foreach (var obj in _createdPortals) Destroy(obj);
            _createdPortals.Clear();
        }
    }
}