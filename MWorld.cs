using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Terraria;
using TAPI;

namespace HallowedEnd {
    [GlobalMod] public class MWorld : ModWorld {
        const int HALLOWEDENDRIFTEVENTS = 2;//3;
        public enum riftNightFlags : int {
           NONE = 0,
           CNC = 1,
           GOW = 2,
           //HELLRAIN = 3
        };
    
        public bool isRiftNight;
        public int riftNightFlag;
        
        int counterTicks;
        bool dnFlag;
        
        bool initFlag;
    
		public MWorld(ModBase modbase) : base(modbase) {
            isRiftNight = false;
            dnFlag = Main.dayTime; //This is used to update day/night
            initFlag = false;
            counterTicks = 0;
            riftNightFlag = (int)riftNightFlags.NONE;
        }

		public override void Initialize() {
            if(!initFlag) {
                Main.NewText("World Loaded: Welcome to Hallowed End Mod [Phantom139].", 255, 0, 0, true);
            }
            //Enable chat.
            Main.allowChat = true;
        } //Called when the world is initialized.
  
		public override void PostUpdate() {
            counterTicks++;
            if(Main.dayTime && !dnFlag) {
                dnFlag = true;
                worldDayTasks();
            }
            if(!Main.dayTime && dnFlag) {
                dnFlag = false;
                worldNightTasks();
            }
            //
            if(isRiftNight && counterTicks == 1000) {
                beginSpacialRift();
            }
            //
            if(isRiftNight) {
                doRiftNightTasks();
            }
        } //Called after the world Update hook.
  
		public override bool? CheckChristmas() { return null; } //If this returns true, it acts like it is christmas. If it returns null, it uses normal code.
  
		public override bool? CheckHalloween() { return null; } //If this returns true, it acts like it is halloween. If it returns null, it uses normal code.
  
		public override void WorldGenPostInit() { } //Called just before the world generates.
  
		public override void WorldGenModifyTaskList(List<WorldGenTask> list) { } //Called to modify the world gen task list.
  
		public override void WorldGenModifyHardmodeTaskList(List<WorldGenTask> list) { } //Called to modify the hardmode world gen task list.
  
        //Phantom: New Code Here
        public void worldDayTasks() {
            //Called when a world enters day
            if(isRiftNight) {
                switch(riftNightFlag) {
                    case (int)riftNightFlags.CNC:
                        Main.NewText("The dimensional breach closes and the futuristic fight ends on the plain...", 255, 0, 0, true);
                        break;

                    case (int)riftNightFlags.GOW:
                        Main.NewText("The dimensional breach closes and the traces of the locust vanish...", 255, 0, 0, true);
                        break;

                    //case (int)riftNightFlags.HELLRAIN:
                    //    Main.NewText("The dimensional breach closes and the rain of fire ends...", 255, 0, 0, true);
                    //    break;
                }
            }
            isRiftNight = false;
            riftNightFlag = (int)riftNightFlags.NONE;
            counterTicks = 0;
        }
        
        public void worldNightTasks() {
            counterTicks = 0;
            //Called when a world enters night
            if(true) { //Main.rand.Next(20) == 10) {
                //Rift Night.
                Main.NewText("A disturbance in the spacial fields sends a chilling wind across the plains", 255, 0, 0, true);
                isRiftNight = true;
            }
        }
        
        public void beginSpacialRift() {
            int universe = Main.rand.Next(1, HALLOWEDENDRIFTEVENTS+1);
            switch(universe) {
                case 1:
                    //C&C Blend
                    Main.NewText("A spacial rift has opened, you can hear the sounds of a futuristic conflict echoing through the world.", 255, 0, 0, true);
                    riftNightFlag = (int)riftNightFlags.CNC;
                    break;
                    
                case 2:
                    //GoW Stuffs
                    Main.NewText("A spacial rift has opened, you can hear the sounds of a seemingly endless war stretching across the fabric of the world.", 255, 0, 0, true);
                    riftNightFlag = (int)riftNightFlags.GOW;
                    break;
                    
                //case 3:
                //    //Hell-Rain: Very Bad Thingy :D
                //    Main.NewText("You hear a loud explosion as the rift collapses over a burning star and a wave of fire begins to screech down.", 255, 0, 0, true);
                //    riftNightFlag = (int)riftNightFlags.HELLRAIN;
                //    break;
            }
        }

        public void doRiftNightTasks() {
            switch(riftNightFlag) {
                case (int)riftNightFlags.CNC:
                    // Do C&C Stuff
                    if(counterTicks % 350 == 0) {
                        if(Main.rand.Next(15) == 1) {
                            if (NPC.CountNPCS("HallowedEnd:Venom") < 4) {
                                int s = Main.rand.Next(Main.numPlayers);
                                if(Main.player[s].active) {
                                    float X = ((float)Main.player[s].position.X)-200;
                                    float Y = ((float)Main.player[s].position.Y)-150;
                                    if(Main.netMode != 1) {
                                       int npcID = NPC.NewNPC((int)X, (int)Y, Defs.npcs["HallowedEnd:Venom"].type, 0);
                                       if (Main.netMode == 2) {
                                           NetMessage.SendData(23, -1, -1, "", npcID, 0.0f, 0.0f, 0.0f, 0);
                                       }
                                    }
                                }
                            }
                        }
                    }
                    if(counterTicks == 2500) {
                        //Here's where C&C Fun Times begin...
                        if(Main.rand.Next(5) == 1) {
                            Main.NewText("NOD has deployed their elite soldier 'The Awakened One' to the field.", 255, 0, 0);
                            foreach(Player p in Main.player) if(p.active) {
                                //Spawn.
                                float X = ((float)p.position.X);
                                float Y = ((float)p.position.Y)-150;
                                if(Main.netMode != 1) {
                                   int npcID = NPC.NewNPC((int)X, (int)Y, Defs.npcs["HallowedEnd:TheAwakened"].type, 0);
                                   if (Main.netMode == 2) {
                                      NetMessage.SendData(23, -1, -1, "", npcID, 0.0f, 0.0f, 0.0f, 0);
                                   }
                                }
                                break;
                            }
                        }
                    }
                    break;
                    
                case (int)riftNightFlags.GOW:
                    // Do GoW Stuff
                    if(counterTicks == 500) {
                       //The theron town bot arrives :)
                       foreach(Player p in Main.player) if(p.active) {
                          //if (!NPC.AnyNPCs("HallowedEnd:TheronTownBot")) {
                          if (NPC.CountNPCS("HallowedEnd:TheronTownBot") <= 0) {
                             Main.NewText("A Theron Guard approaches your area and is pleased with the construction of your 'base'.", 255, 0, 0);
                             if(Main.netMode != 1) {
                                int npcID = NPC.NewNPC((int)p.Center.X, (int)p.Center.Y - 70, Defs.npcs["HallowedEnd:TheronTownBot"].type, 0);
                                if (Main.netMode == 2) {
                                   NetMessage.SendData(23, -1, -1, "", npcID, 0.0f, 0.0f, 0.0f, 0);
                                }
                             }
                          }
                       }
                    }
                    break;

            }
        }
    }
}
