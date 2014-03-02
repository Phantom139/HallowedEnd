using System;
using TAPI;

namespace HallowedEnd {
	public class nodsymbol : ModItem {
        public nodsymbol(ModBase modbase, Item I) : base(modbase, I) { }

        public override void UseItem(Player p) {
            float X = ((float)Main.player[item.owner].position.X);
            float Y = ((float)Main.player[item.owner].position.Y)-150;

            Main.NewText("The Awakened One Has Been Released, The Brotherhood of Nod shall take everything...", 255, 0, 0, true);
            int npcID = NPC.NewNPC((int)X, (int)Y, Defs.npcs["HallowedEnd:TheAwakened"].type, 0);
        }
    }
}
