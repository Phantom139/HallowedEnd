using System;

using Terraria;
using TAPI;
using Microsoft.Xna.Framework;

namespace HallowedEnd {
	public class ObeliskCannon : ModItem {

        public ObeliskCannon(ModBase modbase, Item I) : base(modbase, I) { }

        public override bool PreShoot(Player p, Vector2 Sp, Vector2 Sv, int pid, int damage, float knock) {
           int newProj = Projectile.NewProjectile(Sp.X, Sp.Y, Sv.X, Sv.Y, pid, damage, knock, p.whoAmI);
           Main.projectile[newProj].friendly = true;
           Main.projectile[newProj].hostile = false;
           
           return false;
        }
    }
}
