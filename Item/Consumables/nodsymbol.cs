using System;

using Terraria;
using TAPI;

namespace HallowedEnd {
	public class nodsymbol : ModItem {
        public nodsymbol(ModBase modbase, Item I) : base(modbase, I) { }

        public override bool? UseItem(Player p) {
            float X = ((float)Main.player[item.owner].position.X);
            float Y = ((float)Main.player[item.owner].position.Y)-150;

            Main.NewText("The Awakened One Has Been Released, The Brotherhood of Nod shall take everything...", 255, 0, 0, true);
            if(Main.netMode != 1) {
               int npcID = NPC.NewNPC((int)X, (int)Y, Defs.npcs["HallowedEnd:TheAwakened"].type, 0);
               if (Main.netMode == 2) {
                  NetMessage.SendData(23, -1, -1, "", npcID, 0.0f, 0.0f, 0.0f, 0);
               }
            }
            
            return true;
        }
    }
}
