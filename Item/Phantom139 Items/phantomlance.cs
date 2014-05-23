using System;

using Terraria;
using TAPI;

namespace HallowedEnd {
	public class phantomlance : ModItem {
 
        public phantomlance(ModBase modbase, Item I) : base(modbase, I) { }
 
        public override bool? UseItem(Player p) {
            if(String.Compare(p.name, "Phantom139") != 0) {
               p.KillMe(9000, 0, false, " cannot wield the destructive power");
               return false;
            }
            //
            
            return true;
        }

    }
}
