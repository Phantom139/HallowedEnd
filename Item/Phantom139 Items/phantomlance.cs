using System;
using TAPI;

namespace HallowedEnd {
	public class phantomlance : ModItem {
 
        public phantomlance(ModBase modbase, Item I) : base(modbase, I) { }
 
        public override void UseItem(Player p) {
            if(String.Compare(p.name, "Phantom139") != 0) {
               p.KillMe(9000, 0, false, " cannot wield the destructive power");
               return;
            }
            //
            Main.NewText("Phantom139 lifts his great sword and the time gateway reverses.", 255, 0, 0, true);
            if(Main.netMode != 1){ Main.dayTime = !Main.dayTime; }
            if(Main.netMode != 2){ Main.PlaySound(2, (int)p.Center.X, (int)p.Center.Y, 37); }
        }

    }
}
