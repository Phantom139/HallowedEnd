using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;

namespace HallowedEnd {
    public class TheAwakened : MNPC {
        int abilityCounter;
        int obeliskCounter;
    
        public TheAwakened(ModBase modBase, NPC n) : base(modBase, n) {
            abilityCounter = 0;
            obeliskCounter = 0;
        }

        public override void AI() {
            NPCDoMove(0, 2.0f, 6.8f, 0.22f, 0.22f);
            abilityCounter++;

            if (obeliskCounter < 3) {
                if (abilityCounter >= 300) {
                    // Time to summon a deathy troll tower :D
                    int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Defs.npcs["HallowedEnd:ObeliskOfLight"].type, 0);
                    // Poof Effect :D
                    int dustID = Dust.NewDust(npc.Center, 30, 70, 60, 0.2f, 0.2f, 100, Color.Red, 3.2f);
                    //Maximum of 3 obelisks...
                    obeliskCounter++;
                    abilityCounter = 0;
                }
            }
            else {
                //We're at our maximum , start firing laz0rs
                if (abilityCounter >= 90) {
                    abilityCounter = 0;
                    Vector2 tP = new Vector2(Main.player[npc.target].position.X + (Main.player[npc.target].width/2), Main.player[npc.target].position.Y + (Main.player[npc.target].height/2));
                    Vector2 sP = new Vector2(npc.position.X, npc.position.Y - (npc.height/3));
                    Vector2 target = tP - sP;
                    target.Normalize();
                    target *= 15;
                    int pID = Defs.projectiles["HallowedEnd:nodsoldierlaser"].type;
                    int mahLazor = Projectile.NewProjectile(sP.X, sP.Y, target.X, target.Y, pID, 20, 5, 0);
                    Main.PlaySound(33, (int) npc.position.X, (int) npc.position.Y, 17);
                }
            }
        }
    }
}
