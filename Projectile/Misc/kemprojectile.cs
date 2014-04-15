using System;
using System.Collections.Generic;
using System.Text;

using Terraria;
using TAPI;

namespace HallowedEnd {
    public class kemprojectile : ModProjectile {
        public kemprojectile(ModBase modbase, Projectile P) : base(modbase, P) { }

        public override void AI() {
           //spawn explosives...
           //int plrID = proj.owner;
           float X = (float)((projectile.position.X)+(projectile.width/2));
           float Y = (float)((projectile.position.Y)+(projectile.height/2));
           float VelX = (float)Main.rand.Next(2,4);
           float VelY = (float)Main.rand.Next(-4,-2);
           //28 - Bomb, 29 - Dynamite, 30 - Grenade
           int e = Projectile.NewProjectile(X, Y, VelX, VelY, 28, 0, 0, 0);
           Main.projectile[e].timeLeft = 5;  //Detonate instantly....
        }
    }
}
