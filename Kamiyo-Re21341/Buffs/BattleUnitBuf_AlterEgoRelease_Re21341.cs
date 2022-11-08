using Battle.CreatureEffect;
using BigDLL4221.Buffs;
using LOR_DiceSystem;
using Sound;
using UnityEngine;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_AlterEgoRelease_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        private const string Path = "6/RedHood_Emotion_Aura";
        private CreatureEffect _aura;

        public BattleUnitBuf_AlterEgoRelease_Re21341() : base(infinite: true, lastOneScene: false)
        {
        }

        public override bool isAssimilation => true;
        protected override string keywordId => "AlterEgoMask_Re21341";
        protected override string keywordIconId => "AlterEgoMask_Re21341";
        public override int MaxStack => 10;
        public override int MinStack => 1;
        public override int AdderStackEachScene => -1;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = stack > 4 ? 2 : 1
                });
            if (stack > 7 && behavior.Detail == BehaviourDetail.Evasion)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = 1
                    });
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            PlayChangingEffect(owner);
        }

        public override void OnRoundStart()
        {
            _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 3, _owner);
            if (stack > 4) _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 1, _owner);
            if (stack > 7) _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 2, _owner);
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

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            OnAddBuf(1);
        }
    }
}