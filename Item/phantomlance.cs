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
        }

    }
}
