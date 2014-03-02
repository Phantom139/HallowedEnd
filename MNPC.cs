using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;

namespace HallowedEnd {
    [GlobalMod] public class MNPC : ModNPC {
        //Phantom139: I'd rather save the npc.ai[] for important cases, so here's some predefs used by the ai for movement stuff
        float moveLockCounter;
        float tileLockCounter;
        float genericLockCounter;
    
        public MNPC(ModBase modBase, NPC n) : base(modBase, n) {
            moveLockCounter = 0.0f;
            tileLockCounter = 0.0f;
            genericLockCounter = 0.0f;
        } //NOTE: this npc instance is null, and doesn't actually spawn, this is simply a template class...

        public override List<int> EditSpawnPool(List<int> pool) {
            //Edit here...
            return pool;
        }
        
        //Generic NPC Move Function
        //I have adapted some of this code from the old Obsidian mod, and have improved the code a bit for
        // purposes of readability, usability, and overall performance.
        // * You can use this code for an NPC so long as the NPC bases from MNPC instead of ModNPC.
        public void NPCDoMove(int type, float maxSpeed, float maxJump, float maxAccX, float maxAccY) {
            npc.TargetClosest(true);
            switch(type) {
                //0: Generic Walk Towards Target
                case 0:
                    //Defines
                    int delayTicks = 60;
                    bool movelock = false;
                    bool tileblock = false;
                    bool opendoor = false;
                    bool isgenericlocked = false;
                    //
                    int tileIndexUp = 0;
                    int tileIndexLeft = 0;
                    int tileIndexRight = 0;
                    int tileIndex_xMove = 0;
                    int tileIndex_yMove = 0;
                    //Check for non-movement due to potential move lock
                    if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0))) {
                        movelock = true;
                    }
                    //Confirm a move lock
                    if (npc.position.X == npc.oldPosition.X || moveLockCounter >= (float)delayTicks || movelock) {
                        moveLockCounter += 1f;
                    }
                    else {
                        //Check to see if we're just not moving, if so, it's not a move lock...
                        if ((double)Math.Abs(npc.velocity.X) > 0.9 && moveLockCounter > 0f) {
                            moveLockCounter -= 1f;
                        }
                    }
                    //What to do if move-locked and something happens...
                    if (moveLockCounter > (float)(delayTicks * 10)) {
                        moveLockCounter = 0f;
                    }
                    if (npc.justHit) {
                        moveLockCounter = 0f;
                    }
                    if (moveLockCounter == (float)delayTicks) {
                        npc.netUpdate = true;
                    }
                    //Movement Base
                    if (npc.velocity.X < - maxSpeed || npc.velocity.X > maxSpeed) {
                        if (npc.velocity.Y == 0f) {
                            npc.velocity *= 0.8f;  //<-- constant for 'y' stop, adjust as necessary
                        }
                    }
                    else {
                        if (npc.velocity.X < maxSpeed && npc.direction == 1) {
                            npc.velocity.X = npc.velocity.X + maxAccX;
                            if (npc.velocity.X > maxSpeed) {
                                npc.velocity.X = maxSpeed;
                            }
                        }
                        else {
                            if (npc.velocity.X > -maxSpeed && npc.direction == -1) {
                                npc.velocity.X = npc.velocity.X - maxAccX;
                                if (npc.velocity.X < -maxSpeed) {
                                    npc.velocity.X = -maxSpeed;
                                }
                            }
                        }
                    }
                    //Tiles
                    if (npc.velocity.Y == 0f) {
                        tileIndexUp = (int)(npc.position.Y + (float)npc.height + 8f) / 16;
                        tileIndexLeft = (int)npc.position.X / 16;
                        tileIndexRight = (int)(npc.position.X + (float)npc.width) / 16;
                        for (int i = tileIndexLeft; i <= tileIndexRight; i++) {
                            if (Main.tile[i, tileIndexUp] == null) {
                                //No tiles blocking, we're done!
                                return;
                            }
                            if (Main.tile[i, tileIndexUp].active() && TileDef.solid[Main.tile[i, tileIndexUp].type]) {
                                //Main.tileSolid[Main.tile[i, tileIndexUp].type]) {
                                //We have a blocking tile, so more calcuation work is needed.
                                tileblock = true;
                                break;
                            }
                        }
                    }
                    if (tileblock) {
                        tileIndex_xMove = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
                        tileIndex_yMove = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
                        if (npc.type == 109) {
                            //Clown
                            tileIndex_xMove = (int)((npc.position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 16) * npc.direction)) / 16f);
                        }
                        if (Main.tile[tileIndex_xMove, tileIndex_yMove] == null) {
                            Main.tile[tileIndex_xMove, tileIndex_yMove] = new Tile();
                        }
                        if (Main.tile[tileIndex_xMove, tileIndex_yMove - 1] == null) {
                            Main.tile[tileIndex_xMove, tileIndex_yMove - 1] = new Tile();
                        }
                        if (Main.tile[tileIndex_xMove, tileIndex_yMove - 2] == null) {
                            Main.tile[tileIndex_xMove, tileIndex_yMove - 2] = new Tile();
                        }
                        if (Main.tile[tileIndex_xMove, tileIndex_yMove - 3] == null) {
                            Main.tile[tileIndex_xMove, tileIndex_yMove - 3] = new Tile();
                        }
                        if (Main.tile[tileIndex_xMove, tileIndex_yMove + 1] == null) {
                            Main.tile[tileIndex_xMove, tileIndex_yMove + 1] = new Tile();
                        }
                        if (Main.tile[tileIndex_xMove + npc.direction, tileIndex_yMove - 1] == null) {
                            Main.tile[tileIndex_xMove + npc.direction, tileIndex_yMove - 1] = new Tile();
                        }
                        if (Main.tile[tileIndex_xMove + npc.direction, tileIndex_yMove + 1] == null) {
                            Main.tile[tileIndex_xMove + npc.direction, tileIndex_yMove + 1] = new Tile();
                        }
                        if (Main.tile[tileIndex_xMove, tileIndex_yMove - 1].active() && Main.tile[tileIndex_xMove, tileIndex_yMove - 1].type == 10 && tileblock) {
                            tileLockCounter += 1f;
                            moveLockCounter = 0f;
                            if (tileLockCounter >= 60f) {
                                npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
                                genericLockCounter += 1f;
                                tileLockCounter = 0f;
                                if (genericLockCounter >= 10f) {
                                    isgenericlocked = true;
                                    genericLockCounter = 10f;
                                }
                                WorldGen.KillTile(tileIndex_xMove, tileIndex_yMove - 1, true, false, false);
                                if ((Main.netMode != 1 || !isgenericlocked) && isgenericlocked && Main.netMode != 1) {
                                    if (npc.type == 26) {
                                        WorldGen.KillTile(tileIndex_xMove, tileIndex_yMove - 1, false, false, false);
                                        if (Main.netMode == 2) {
                                            NetMessage.SendData(17, -1, -1, "", 0, (float)tileIndex_xMove, (float)(tileIndex_yMove - 1), 0f, 0);
                                        }
                                    }
                                    else {
                                        opendoor = WorldGen.OpenDoor(tileIndex_xMove, tileIndex_yMove, npc.direction);
                                        if (!opendoor) {
                                            moveLockCounter = (float)delayTicks;
                                            npc.netUpdate = true;
                                        }
                                        if (Main.netMode == 2 && opendoor) {
                                            NetMessage.SendData(19, -1, -1, "", 0, (float)tileIndex_xMove, (float)tileIndex_yMove, (float)npc.direction, 0);
                                        }
                                    }
                                }
                            }
                        }
                        else {
                            if ((npc.velocity.X < 0f && npc.spriteDirection == -1) || (npc.velocity.X > 0f && npc.spriteDirection == 1)) {
                                if (Main.tile[tileIndex_xMove, tileIndex_yMove - 2].active() && TileDef.solid[(int)Main.tile[tileIndex_xMove, tileIndex_yMove - 2].type]) {
                                    if (Main.tile[tileIndex_xMove, tileIndex_yMove - 3].active() && TileDef.solid[(int)Main.tile[tileIndex_xMove, tileIndex_yMove - 3].type]) {
                                        npc.velocity.Y = -(maxJump + 3);
                                        npc.netUpdate = true;
                                    }
                                    else {
                                        npc.velocity.Y = -(maxJump + 2);
                                        npc.netUpdate = true;
                                    }
                                }
                                else {
                                    if (Main.tile[tileIndex_xMove, tileIndex_yMove - 1].active() && TileDef.solid[(int)Main.tile[tileIndex_xMove, tileIndex_yMove - 1].type]) {
                                        npc.velocity.Y = -(maxJump + 1);
                                        npc.netUpdate = true;
                                    }
                                    else {
                                        if (Main.tile[tileIndex_xMove, tileIndex_yMove].active() && TileDef.solid[(int)Main.tile[tileIndex_xMove, tileIndex_yMove].type]) {
                                            npc.velocity.Y = -(maxJump);
                                            npc.netUpdate = true;
                                        }
                                        else {
                                            if (npc.directionY < 0 && npc.type != 67 && (!Main.tile[tileIndex_xMove, tileIndex_yMove + 1].active() || !TileDef.solid[(int)Main.tile[tileIndex_xMove, tileIndex_yMove + 1].type]) && (!Main.tile[tileIndex_xMove + npc.direction, tileIndex_yMove + 1].active() || !TileDef.solid[(int)Main.tile[tileIndex_xMove + npc.direction, tileIndex_yMove + 1].type])) {
                                                npc.velocity.Y = -maxJump;
                                                npc.velocity.X = npc.velocity.X * maxJump;
                                                npc.netUpdate = true;
                                            }
                                            else {
                                                if (tileblock) {
                                                    genericLockCounter = 0f;
                                                    tileLockCounter = 0f;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else {
                        if (tileblock) {
                            genericLockCounter = 0f;
                            tileLockCounter = 0f;
                        }
                    }
                    break;
                    
                //1: Flight
                case 1:
                    if (Main.player[npc.target].position.X < npc.position.X) {
                        if (npc.velocity.X > -(maxSpeed)) {
                            npc.velocity.X -= maxAccX;
                        }
                    }

                    if (Main.player[npc.target].position.X > npc.position.X) {
                        if (npc.velocity.X < maxSpeed) {
                            npc.velocity.X += maxAccX;
                        }
                    }

                    if (Main.player[npc.target].position.Y < npc.position.Y + 5) { //Note: +5 places the NPC slightly under the center of a player, adjust as necessary :)
                        if (npc.velocity.Y < 0) {
                            if (npc.velocity.Y > -(maxSpeed)) {
                                npc.velocity.Y -= maxAccY - 0.1f;
                            }
                        }
                        else {
                            npc.velocity.Y -= maxAccY;
                        }
                    }

                    if (Main.player[npc.target].position.Y > npc.position.Y + 5) {
                        if (npc.velocity.Y > 0) {
                            if (npc.velocity.Y < maxSpeed) {
                                npc.velocity.Y += maxAccY - 0.1f;
                            }
                        }
                        else {
                            npc.velocity.Y += maxAccY;
                        }
                    }
                    break;
                
            }
        }
    }
}
