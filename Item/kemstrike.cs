using System;
using System.Timers;
using TAPI;

namespace TAPI.HallowedEnd {
	public class kemstrike : ModItem {
        private static System.Timers.Timer kemArrivalTimer;
 
        public kemstrike(ModBase modbase, Item I) : base(modbase, I) { }
 
        public override void UseItem(Player p) {
            float X = ((float)Main.player[item.owner].position.X);
            float Y = ((float)Main.player[item.owner].position.Y)-10;
            kemArrivalTimer = new System.Timers.Timer(5000);
            kemArrivalTimer.Elapsed += (sender, args) => OnTimeComplete(sender, args, p, X, Y);
            kemArrivalTimer.Enabled = true;
            Main.NewText("KEM STRIKE IN 5 SECONDS.....", 255, 0, 0, true);
        }
        
        public void OnTimeComplete(object src, ElapsedEventArgs a, Player p, float X, float Y) {
            initializeKEMStrike(X, Y, p);
        }
        
        public void initializeKEMStrike(float X, float Y, Player p) {
            float VelX = (float)0;
            float VelY = (float)-1;
            int kemid = Defs.projectiles["HallowedEnd:kemprojectile"].type;
            int SpawnKem = Projectile.NewProjectile(X, Y, VelX, VelY, kemid, 0, 0, p.whoAmI);
            kemArrivalTimer.Enabled = false;
        }
    }
}
