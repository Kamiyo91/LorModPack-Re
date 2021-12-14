using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using HarmonyLib;
using Sound;

namespace Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_PossessorOfMioLight : BattleUnitBuf
    {
        public BattleUnitBuf_PossessorOfMioLight() => stack = 0;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "MioLight_Re21341";
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().FindAll(x => x.GetID() == new LorId(ModParameters.PackageId,22) || x.GetID() == new LorId(ModParameters.PackageId,19)))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
            typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all)
                ?.SetValue(this, ModParameters.ArtWorks["Light_Re21341"]);
            typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all)?.SetValue(this, true);
            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "5_T/FX_IllusionCard_5_T_Happiness", 1f, _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
        }
        public override void OnRoundStartAfter() => _owner.cardSlotDetail.RecoverPlayPoint(1);
        public override void OnRoundEnd() => RecoverHpAndStagger();

        private void RecoverHpAndStagger()
        {
            _owner.RecoverHP(3);
            _owner.breakDetail.RecoverBreak(3);
        }
    }
}
