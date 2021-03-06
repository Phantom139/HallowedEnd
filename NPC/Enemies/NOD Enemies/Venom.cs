using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Terraria;
using TAPI;

namespace HallowedEnd {
    public class Venom : MNPC {
        int abilityCounter;
        
        float maxSpeed = 2f;
        float maxAccX = 0.12f;
        float maxAccY = 0.3f;
        
        bool leftRight = false; //Left
    
        public Venom(ModBase modBase, NPC n) : base(modBase, n) {
            abilityCounter = 0;
        }

        public override void AI() {
            npc.TargetClosest(true);
            abilityCounter++;
            
            //This chopper likes to sit away from the victim and fire off machine guns
            if(abilityCounter > 107) {
                //Fire mah machine gun
                Vector2 tP = new Vector2(Main.player[npc.target].position.X + (Main.player[npc.target].width/2), Main.player[npc.target].position.Y + (Main.player[npc.target].height/2));
                Vector2 sP = new Vector2(npc.position.X, npc.position.Y - (npc.height/3));
                Vector2 target = tP - sP;
                target.Normalize();
                target *= 15;
                int pID = 104; //Defs.projectiles["HallowedEnd:nodsoldierlaser"].type;
                if(Main.netMode != 1) {
                    int bullet = Projectile.NewProjectile(sP.X, sP.Y, target.X, target.Y, pID, 10, 5, 0);
                    Main.projectile[bullet].friendly = false;
                    Main.projectile[bullet].hostile = true;
                }
                if(abilityCounter >= 115) {
                    abilityCounter = 0;
                    if(Main.rand.Next(3) == 1) {
                        leftRight = !leftRight;
                    }
                }
            }
            
            //Prefer Left
            if(leftRight) {
                if (Main.player[npc.target].position.X < npc.position.X - 400) {
                    if (npc.velocity.X > -(maxSpeed)) {
                        npc.velocity.X -= maxAccX;
                    }
                }
            }
            //Prefer Right
            else {
                if (Main.player[npc.target].position.X > npc.position.X + 400) {
                    if (npc.velocity.X < maxSpeed) {
                        npc.velocity.X += maxAccX;
                    }
                }
            }
            if (Main.player[npc.target].position.Y < npc.position.Y + 250) {
                if (npc.velocity.Y < 0) {
                    if (npc.velocity.Y > -(maxSpeed)) {
                        npc.velocity.Y -= maxAccY - 0.1f;
                    }
                }
                else {
                    npc.velocity.Y -= maxAccY;
                }
            }
            if (Main.player[npc.target].position.Y > npc.position.Y + 250) {
                if (npc.velocity.Y > 0) {
                    if (npc.velocity.Y < maxSpeed) {
                        npc.velocity.Y += maxAccY - 0.1f;
                    }
                }
                else {
                    npc.velocity.Y += maxAccY;
                }
            }
        }
    }
}
