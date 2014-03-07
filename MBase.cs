using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;

namespace HallowedEnd {
    public class MBase : TAPI.ModBase { //MBase can be any name
        public override void OnLoad(){} //Called when the mod is first loaded. 
        public override void OnUnload(){} //Called when the mod is unloaded.
        public override void OnAllModsLoaded(){} //Called when all mods are loaded.
        public override void PostGameDraw(SpriteBatch sb) { } //Called after all game drawing is finished.
        public override void ChooseTrack(ref string current){} //Called when the music track updates.

        public override object OnModCall(TAPI.ModBase mod, params object[] args) { return base.OnModCall(mod, args);  } //Called when another mod calls this on your mod. (used for inter-mod communicating)
        public override void NetReceive(int msgType, BinBuffer bb){} //Used to handle networking.
    }
}
