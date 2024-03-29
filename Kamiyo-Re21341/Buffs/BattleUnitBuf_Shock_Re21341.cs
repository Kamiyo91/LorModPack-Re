﻿using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Buffs
{
    public class BattleUnitBuf_Shock_Re21341 : BattleUnitBuf
    {
        private bool _clashWin;

        protected override string keywordId => _owner.Book.BookId.packageId == KamiyoModParameters.PackageId &&
                                               (_owner.Book.BookId.id == 4 || _owner.Book.BookId.id == 10000004 ||
                                                _owner.Book.BookId.id == 10000901)
            ? "Shock_Re21341"
            : "ShockGeneral_Re21341";

        protected override string keywordIconId => "Shock_Re21341";

        public override void OnRoundStart()
        {
            if (stack > 0 && !_owner.bufListDetail.HasBuf<BattleUnitBuf_AlterEgoRelease_Re21341>() &&
                _owner.Book.BookId.packageId == KamiyoModParameters.PackageId) _owner.TakeBreakDamage(2);
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            _clashWin = true;
        }

        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            if (!_clashWin) return;
            _clashWin = false;
            this.AddBufCustom(1, maxStack: 20);
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return stack > 19 ? -1 : base.GetCardCostAdder(card);
        }
    }
}