using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Terraria;
using TAPI;

namespace HallowedEnd {
    public class TheronTownBot : ModNPC {
        public TheronTownBot(ModBase modBase, NPC n) : base(modBase, n){ }
        
		public override void HitEffect(int hitDirection, double damage, bool isDead) {
			for (int m = 0; m < (isDead ? 20 : 5); m++) {
				int dustID = Dust.NewDust(npc.position, npc.width, npc.height, 5, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, isDead && m % 2 == 0 ? 3f : 1f);
				if (isDead && m % 2 == 0) { Main.dust[dustID].noGravity = true; }
			}
			if (isDead) {
               Gore.NewGore(npc.position, npc.velocity, Defs.gores["HallowedEnd:TheronNPCHead"], 1f);
               Gore.NewGore(npc.position, npc.velocity, Defs.gores["HallowedEnd:TheronNPCBody"], 1f);
               Gore.NewGore(npc.position, npc.velocity, Defs.gores["HallowedEnd:TheronNPCLeg"], 1f);
               Gore.NewGore(npc.position, npc.velocity, Defs.gores["HallowedEnd:TheronNPCLeg"], 1f);
			}
		}

        public override string SetName() {
            int rand = Main.rand.Next(4);
            string name = "";
            switch(rand) {
                case 0: name = "Bob"; break;
                case 1: name = "Matt"; break;
                case 2: name = "Alex"; break;
                default: name = "David"; break;
            }
            return name;
        }

        public override string SetChat()  {
            int rand = Main.rand.Next(4);
            string text = "";
            switch (rand) {
                case 0: text = "Those wispers are most certainly not the wind..."; break;
                case 1: text = "GROUNDWAL... Oh wait, you have cash for me..."; break;
                case 2: text = "I see you eyeing my Torque Bow.. You can't have it, it's mine!!!"; break;
                default: text = "I like hissing, but I think you noticed that."; break;
            }
            string guidesName = NPC.AnyNPCs("Guide") ? Main.chrName[22] : null;
            if (guidesName != null && Main.rand.Next(4) == 0){ text = "We therons don't approve much of " + guidesName + ". We make voodoo dolls of him but they keep getting stolen.."; }

            if(((MWorld)modBase.modWorld).riftNightFlag == (int)MWorld.riftNightFlags.CNC && Main.rand.Next(4) == 0) { text = "I once saw this wierd tower thingy with a red crystal on it, It started glowing violently, so I ran away."; }

            return text;
        }
        
        public override bool CanTownNPCSpawn() {
            return true;
        }

        public override void SetupShop(Chest chest, ref int index)  {
            int num = 0;
            chest.item[num].SetDefaults("HallowedEnd:torquebow");
            num++;
            chest.item[num].SetDefaults("HallowedEnd:immulsionarrow");
            num++; 
        }
        public override bool ResetShop(Chest chest) { return true; }

        public override void SetChatButtons(ref string[] buttons)  {
            buttons[0] = "Shop";
            buttons[1] = "Souls";
        }

        public override Action SetChatButtonAction(string[] buttons, int buttonIndex) {
            if (buttonIndex == 0) {
				return null;
			}
            else if (buttonIndex == 1) {
                return () => {
                   Player p = Main.player[Main.myPlayer];
                   if(String.Compare(p.name, "Occult") != 0) {
                       Main.npcChatText = "I WILL HAVE YOUR SOUL!!!";
                       //Summon Soul Stealing Theron Invasion
                   }
                   else {
                       Main.npcChatText = "Souls? Did I say I will have your soul? No, I couldn't have...";
                   }
                };
            }
            return null;
        }

    }
}
