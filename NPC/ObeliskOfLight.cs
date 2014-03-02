using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;

namespace HallowedEnd {
    public class ObeliskOfLight : ModNPC {
        public ObeliskOfLight(ModBase modBase, NPC n) : base(modBase, n){ }

        public override void AI() {
            npc.TargetClosest(true);
            if (npc.ai[0] >= 90) {
                npc.ai[0] = 0;
                Vector2 tP = Main.player[npc.target].position;
                Vector2 sP = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Vector2 target = tP - sP;
                int pID = Defs.projectiles["HallowedEnd:nodsoldierlaser"].type;
                int mahLazor = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (target.X*20), (target.Y*20), pID, 0, 0, npc.whoAmI);
                Main.PlaySound(33, (int) npc.position.X, (int) npc.position.Y, 17);
            }
        }
    }
}