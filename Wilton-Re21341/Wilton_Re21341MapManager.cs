﻿using System;
using CustomMapUtility;
using KamiyoModPack.BLL_Re21341.Models;
using UnityEngine;

namespace KamiyoModPack.Wilton_Re21341
{
    public class Wilton_Re21341MapManager : CustomMapManager
    {
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(KamiyoModParameters.PackageId);
        private AudioClip[] _introClip;
        private bool _loop;
        private AudioClip[] _multiClip;
        private bool _snap;
        public int Phase;

        protected override string[] CustomBGMs
        {
            get { return new[] { "Obscurity_Intro.ogg" }; }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MusicCheck();
        }

        public void Update()
        {
            MusicCheck();
        }

        private void MusicCheck()
        {
            if (!isEnabled || !_bMapInitialized) return;
            if (_introClip.Contains(SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.clip))
            {
                if (!SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.isPlaying)
                {
                    SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.clip = _multiClip[0];
                    SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.Play();
                    SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.timeSamples = 0;
                    mapBgm[0] = _multiClip[0];
                    SingletonBehavior<BattleSoundManager>.Instance.SetEnemyThemeIndexZero(_multiClip[0]);
                    Debug.Log("BGM: Exited intro");
                    goto changed;
                }

                if (Phase == 1 && SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.clip ==
                    _introClip[0])
                {
                    if (!_snap)
                    {
                        SingletonBehavior<BattleSoundManager>.Instance.PlaySound(EffectSoundType.CHANGE_THEME,
                            transform.position);
                        _snap = true;
                    }

                    var samples = SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.timeSamples;
                    if (samples > 800000)
                    {
                        SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.clip = _introClip[1];
                        SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.Play();
                        SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.timeSamples = samples;
                        SingletonBehavior<BattleSoundManager>.Instance.SetEnemyThemeIndexZero(_introClip[1]);
                    }
                }

                if (!SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.loop) return;
                Debug.Log("BGM: Intro playing, disabling loop");
                SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.loop = false;
                _loop = false;
                return;
            }

            if (!_loop && !SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.loop)
            {
                Debug.Log("BGM: Music changed, re-enabling loop");
                SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.loop = true;
            }

            changed:
            if (!_multiClip.Contains(SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.clip) ||
                SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.clip == _multiClip[Phase]) return;
            {
                if (!_snap)
                    SingletonBehavior<BattleSoundManager>.Instance.PlaySound(EffectSoundType.CHANGE_THEME,
                        transform.position);
                // snap = true;
                var samples = SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.timeSamples + 583636;
                SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.clip = _multiClip[Phase];
                SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.Play();
                SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.timeSamples = samples;
                mapBgm[0] = _multiClip[Phase];
                SingletonBehavior<BattleSoundManager>.Instance.SetEnemyThemeIndexZero(_multiClip[Phase]);
                Debug.Log($"BGM: PhaseChange to {Phase}");
                _snap = false;
            }
        }

        public override void InitializeMap()
        {
            try
            {
                _cmh.LoadEnemyTheme("Obscurity_Intro.ogg");
                _cmh.LoadEnemyTheme("Obscurity_Intro2.ogg");
                _cmh.LoadEnemyTheme("Obscurity_Phase1_Loop2.ogg");
                _cmh.LoadEnemyTheme("Obscurity_Phase2_Loop.ogg");
                _multiClip = new[]
                {
                    _cmh.GetAudioClip("Obscurity_Phase1_Loop2.ogg"),
                    _cmh.GetAudioClip("Obscurity_Phase2_Loop.ogg")
                };
                _introClip = new[]
                {
                    _cmh.GetAudioClip("Obscurity_Intro.ogg"),
                    _cmh.GetAudioClip("Obscurity_Intro2.ogg")
                };
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + "\n" + ex.InnerException);
            }

            base.InitializeMap();
        }

        public override void EnableMap(bool b)
        {
            if (!_loop && !b)
            {
                Debug.Log("BGM: Map disabled, re-enabling loop");
                SingletonBehavior<BattleSoundManager>.Instance.CurrentPlayingTheme.loop = true;
                _loop = true;
            }

            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}