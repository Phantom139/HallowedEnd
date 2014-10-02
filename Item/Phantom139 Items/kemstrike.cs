using System;
using System.Timers;

using Terraria;
using TAPI;

namespace HallowedEnd {
	public class kemstrike : ModItem {
        private static System.Timers.Timer kemArrivalTimer;
 
        public kemstrike(ModBase modbase, Item I) : base(modbase, I) { }
 
        public override bool? UseItem(Player p) {
            float X = ((float)Main.player[item.owner].position.X);
            float Y = ((float)Main.player[item.owner].position.Y)-10;
            kemArrivalTimer = new System.Timers.Timer(5000);
            kemArrivalTimer.Elapsed += (sender, args) => OnTimeComplete(sender, args, p, X, Y);
            kemArrivalTimer.Enabled = true;
            Main.NewText("KEM STRIKE IN 5 SECONDS.....", 255, 0, 0, true);
            
            return true;
        }
        
        public void OnTimeComplete(object src, ElapsedEventArgs a, Player p, float X, float Y) {
            initializeKEMStrike(X, Y, p);
        }
        
        public void initializeKEMStrike(float X, float Y, Player p) {
            float VelX = (float)0;
            float VelY = (float)-1;
            int kemid = Defs.projectiles["HallowedEnd:kemprojectile"].type;
            if(Main.netMode != 1) {
                int SpawnKem = Projectile.NewProjectile(X, Y, VelX, VelY, kemid, 0, 0, p.whoAmI);
            }
            kemArrivalTimer.Enabled = false;
        }
    }
}
