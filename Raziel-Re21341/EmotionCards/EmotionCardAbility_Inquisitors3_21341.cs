namespace KamiyoModPack.Raziel_Re21341.EmotionCards
{
    public class EmotionCardAbility_Inquisitors3_21341 : EmotionCardAbilityBase
    {
        public override void OnSelectEmotion()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(_owner.faction))
            {
                unit.RecoverHP(25);
                unit.breakDetail.RecoverBreak(25);
            }
        }

        public override void OnRoundStart()
        {
            var units = BattleObjectManager.instance.GetAliveList(_owner.faction);
            if (units.Count <= 2) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(_owner.faction))
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 3, _owner);
        }
    }
}