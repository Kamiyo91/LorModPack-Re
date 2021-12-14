using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using Util_Re21341;

namespace Angela_Re21341.Passives
{
    public class PassiveAbility_AngelaUnit_Re21341 : PassiveAbilityBase
    {
        private BattleDialogueModel _dlg;

        public override void OnWaveStart()
        {
            if (string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) &&
                owner.UnitData.unitData.bookItem == owner.UnitData.unitData.CustomBookItem)
                UnitUtil.PrepareSephirahSkin(owner, 21, ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("AngelaName_Re21341")).Value.Desc, ref _dlg);
            AddCardsWaveStart();
        }

        private void AddCardsWaveStart()
        {
            if (owner.emotionDetail.EmotionLevel == 3)
            {
                owner.personalEgoDetail.AddCard(9910011);
                owner.personalEgoDetail.AddCard(9910012);
                owner.personalEgoDetail.AddCard(9910013);
            }

            if (owner.emotionDetail.EmotionLevel == 4)
            {
                owner.personalEgoDetail.AddCard(9910011);
                owner.personalEgoDetail.AddCard(9910012);
                owner.personalEgoDetail.AddCard(9910013);
                owner.personalEgoDetail.AddCard(9910014);
                owner.personalEgoDetail.AddCard(9910015);
                owner.personalEgoDetail.AddCard(9910016);
            }

            if (owner.emotionDetail.EmotionLevel != 5) return;
            owner.personalEgoDetail.AddCard(9910011);
            owner.personalEgoDetail.AddCard(9910012);
            owner.personalEgoDetail.AddCard(9910013);
            owner.personalEgoDetail.AddCard(9910014);
            owner.personalEgoDetail.AddCard(9910015);
            owner.personalEgoDetail.AddCard(9910016);
            owner.personalEgoDetail.AddCard(9910017);
            owner.personalEgoDetail.AddCard(9910018);
            owner.personalEgoDetail.AddCard(9910019);
        }

        private void AddCardOnLvUpEmotion()
        {
            if (owner.emotionDetail.EmotionLevel == 3)
            {
                owner.personalEgoDetail.AddCard(9910011);
                owner.personalEgoDetail.AddCard(9910012);
                owner.personalEgoDetail.AddCard(9910013);
            }

            if (owner.emotionDetail.EmotionLevel == 4)
            {
                owner.personalEgoDetail.AddCard(9910014);
                owner.personalEgoDetail.AddCard(9910015);
                owner.personalEgoDetail.AddCard(9910016);
            }

            if (owner.emotionDetail.EmotionLevel != 5) return;
            owner.personalEgoDetail.AddCard(9910017);
            owner.personalEgoDetail.AddCard(9910018);
            owner.personalEgoDetail.AddCard(9910019);
        }

        public override void OnLevelUpEmotion() => AddCardOnLvUpEmotion();

        public override void OnBattleEnd()
        {
            if (string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) &&
                owner.UnitData.unitData.bookItem == owner.UnitData.unitData.CustomBookItem)
                UnitUtil.ReturnToTheOriginalBaseSkin(owner, _dlg);
        }
    }
}
