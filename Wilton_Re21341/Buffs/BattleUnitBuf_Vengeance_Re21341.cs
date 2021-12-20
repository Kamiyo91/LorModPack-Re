using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.CreatureEffect;
using BLL_Re21341.Models;
using HarmonyLib;
using Sound;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Wilton_Re21341.Buffs
{
    public class BattleUnitBuf_Vengeance_Re21341 : BattleUnitBuf
    {
        private const string Path = "6/RedHood_Emotion_Aura";
        private CreatureEffect _aura;
        public BattleUnitBuf_Vengeance_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        public override bool isAssimilation => true;
        protected override string keywordId => "Vengeance_Re21341";
        protected override string keywordIconId => "RedHood_Rage";
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            PlayChangingEffect(owner);
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }
        private void PlayChangingEffect(BattleUnitModel owner)
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            if (_aura == null)
                _aura = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect(Path, 1f, owner.view,
                    owner.view);
            var original = Resources.Load("Prefabs/Battle/SpecialEffect/RedMistRelease_ActivateParticle");
            if (original != null)
            {
                var gameObject = Object.Instantiate(original) as GameObject;
                gameObject.transform.parent = owner.view.charAppearance.transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.transform.localScale = Vector3.one;
            }
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

    }
}
