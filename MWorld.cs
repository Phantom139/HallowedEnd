using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;

namespace HallowedEnd {
    [GlobalMod] public class MWorld : ModWorld {
        const int HALLOWEDENDRIFTEVENTS = 3;
        public enum riftNightFlags : int {
           NONE = 0,
           CNC = 1,
           GOW = 2,
           HELLRAIN = 3
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

                    case (int)riftNightFlags.HELLRAIN:
                        Main.NewText("The dimensional breach closes and the rain of fire ends...", 255, 0, 0, true);
                        break;
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
                    
                case 3:
                    //Hell-Rain: Very Bad Thingy :D
                    Main.NewText("You hear a loud explosion as the rift collapses over a burning star and a wave of fire begins to screech down.", 255, 0, 0, true);
                    riftNightFlag = (int)riftNightFlags.HELLRAIN;
                    break;
            }
        }

        public void doRiftNightTasks() {
            Vector2 dir, pos;
        
            switch(riftNightFlag) {
                case (int)riftNightFlags.CNC:
                    // Do C&C Stuff
                    if(counterTicks == 2500) {
                        //Here's where C&C Fun Times begin...
                        if(true) { //Main.rand.Next(5) == 1) {
                            Main.NewText("NOD has deployed their elite soldier 'The Awakened One' to the field.");
                            foreach(Player p in Main.player) if(p.active) {
                                //Spawn.
                                float X = ((float)p.position.X);
                                float Y = ((float)p.position.Y)-150;
                                int npcID = NPC.NewNPC((int)X, (int)Y, Defs.npcs["HallowedEnd:TheAwakened"].type, 0);
                                break;
                            }
                        }
                    }
                    break;
                    
                case (int)riftNightFlags.GOW:
                    // Do GoW Stuff
                    break;
                    
                case (int)riftNightFlags.HELLRAIN:
                    // Do Firestorm Stuff
                    if(counterTicks % 200 == 0) {
                        foreach(Player p in Main.player) if(p.active) {
                            pos = p.position;
                            dir.X = Main.rand.Next(-20, 21);
                            dir.Y = 100; //It's booking it :)
                            dir.Normalize();
                            int projo = Projectile.NewProjectile(pos.X, (pos.Y - (float)2500), dir.X*2, dir.Y*5, 12, 20, 5, 0);
                            Main.projectile[projo].timeLeft = 1200;
                            Main.projectile[projo].friendly = false;
                            Main.projectile[projo].hostile = true;
                        }
                    }
                    if(counterTicks % 50 == 0) {
                        //Spawn another wave of fire near all surface players
                        foreach(Player p in Main.player) if(p.active) {
                            pos = p.position;
                            //if(pos.Y <= Main.worldSurface) {
                                for(int i = 0; i < 3; i++) {
                                    dir.X = Main.rand.Next(-5, 6);
                                    dir.Y = 5;
                                    dir.Normalize();
                                   
                                    int projo = Projectile.NewProjectile(pos.X, (pos.Y - (float)1500), dir.X*2, dir.Y*5, Main.rand.Next(95, 97), 20, 5, 0);
                                    Main.projectile[projo].timeLeft = 600;
                                    Main.projectile[projo].friendly = false;
                                    Main.projectile[projo].hostile = true;
                                }
                            //}
                        }
                    }
                    break;
            }
        }
    }
}
