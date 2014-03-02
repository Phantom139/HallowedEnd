using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;

namespace HallowedEnd {
    public class ModPlayer : TAPI.ModPlayer {
        public ModPlayer(ModBase modBase, Player player) : base(modBase, player) { }

        public override void PostUpdate() {

        } 

        public override void OnInventoryReset(bool mediumcoreRespawn) {

        }

    }
}
