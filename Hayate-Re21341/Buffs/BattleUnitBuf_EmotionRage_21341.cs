using Sound;

namespace KamiyoModPack.Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_EmotionRage_21341 : BattleUnitBuf
    {
        public override bool IsControllable => _owner.faction == Faction.Enemy;

        public override bool TeamKill()
        {
            return true;
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            SoundEffectPlayer.PlaySound("Creature/Angry_Meet");
        }
    }
}