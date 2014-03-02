using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;

namespace HallowedEnd {
    public class TheAwakened : ModNPC {
        public TheAwakened(ModBase modBase, NPC n) : base(modBase, n){ }

        public override void AI() {
            npc.TargetClosest(true);

            if (Main.player[npc.target].position.X < npc.position.X) {
                if (npc.velocity.X > -8) {
                    npc.velocity.X -= 0.22f;
                }
            }

            if (Main.player[npc.target].position.X > npc.position.X) {
                if (npc.velocity.X < 8) {
                    npc.velocity.X += 0.22f;
                }
            }

            if (Main.player[npc.target].position.Y < npc.position.Y+5) {
                if (npc.velocity.Y < 0) {
                    if (npc.velocity.Y > -4) {
                        npc.velocity.Y -= 0.7f;
                    }
                }
                else {
                    npc.velocity.Y -= 0.8f;
                }
            }

            if (Main.player[npc.target].position.Y > npc.position.Y+5) {
                if (npc.velocity.Y > 0) {
                    if (npc.velocity.Y < 4) {
                        npc.velocity.Y += 0.7f;
                    }
                }
                else {
                    npc.velocity.Y += 0.8f;
                }
            }

            npc.ai[0]++;

            if (npc.ai[1] < 3) {
                if (npc.ai[0] >= 500) {
                    // Time to summon a deathy troll tower :D
                    int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Defs.npcs["HallowedEnd:ObeliskOfLight"].type, 0);
                    // Poof Effect :D
                    int dustID = Dust.NewDust(npc.Center, 30, 70, 60, 0.2f, 0.2f, 100, Color.Red, 1.2f);
                    //Maximum of 3 obelisks...
                    npc.ai[1]++;
                    npc.ai[0] = 0;
                }
            }
            else {
                //We're at our maximum , start firing laz0rs
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
}
