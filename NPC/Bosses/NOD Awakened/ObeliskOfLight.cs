using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Terraria;
using TAPI;

namespace HallowedEnd {
    public class ObeliskOfLight : MNPC {
        int abilityCounter;
    
        public ObeliskOfLight(ModBase modBase, NPC n) : base(modBase, n) {
            abilityCounter = 0;
        }

        public override void AI() {
            npc.TargetClosest(true);
            abilityCounter++;
            
            if(abilityCounter >= 45) {
                //create red "charge" dust
                Vector2 chargePos = new Vector2(npc.position.X, npc.position.Y - (npc.height/3));
                if(Main.netMode != 2) {
                    int dust1 = Dust.NewDust(chargePos, 20, 20, 60, 0.0f, 1.0f, 100, Color.Red, 1.2f);
                }
            }
            
            if (abilityCounter >= 90) {
                abilityCounter = 0;
                Vector2 tP = new Vector2(Main.player[npc.target].position.X + (Main.player[npc.target].width/2), Main.player[npc.target].position.Y + (Main.player[npc.target].height/2));
                Vector2 sP = new Vector2(npc.position.X, npc.position.Y - (npc.height/3));
                Vector2 target = tP - sP;
                target.Normalize();
                target *= 15;
                int pID = 100; //Defs.projectiles["HallowedEnd:nodsoldierlaser"].type;
                if(Main.netMode != 1) {
                    int mahLazor = Projectile.NewProjectile(sP.X, sP.Y, target.X, target.Y, pID, 20, 5, 0);
                }
                if(Main.netMode != 2) {
                    Main.PlaySound(33, (int) npc.position.X, (int) npc.position.Y, 17);
                }
            }
        }
    }
}
