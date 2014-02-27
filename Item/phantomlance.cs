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
            //Spawn a theron town bot...
            if (NPC.AnyNPCs("HallowedEnd:TheronTownBot")){ return; }
            int npcID = NPC.NewNPC((int)p.Center.X, (int)p.Center.Y - 200, Defs.npcs["HallowedEnd:TheronTownBot"].type, 0);
        }

    }
}
