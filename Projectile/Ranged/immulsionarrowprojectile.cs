using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using TAPI;

namespace HallowedEnd {
    public class immulsionarrowprojectile : ModProjectile {
        public immulsionarrowprojectile(ModBase modBase, Projectile p) : base(modBase, p){ }

        public override void PostAI() {
            Vector2 center = projectile.Center;
            Vector2 lookTarget = projectile.Center + projectile.velocity;
            float rotX = lookTarget.X - center.X;
            float rotY = lookTarget.Y - center.Y;
            projectile.rotation = -((float)Math.Atan2((double)rotX, (double)rotY)) - 1.57f;
            //Make a red dust
            int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, Color.Red, 1.2f);
        }

        public override void PostKill() {
            int e = Projectile.NewProjectile((int)projectile.position.X, (int)projectile.position.Y, 0, 0, 30, 0, 0, 0);
            Main.projectile[e].timeLeft = 5;  //Detonate instantly....
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
        }
    }
}
