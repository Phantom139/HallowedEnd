using System;
using System.Collections.Generic;
using System.Text;

using TAPI;

namespace HallowedEnd {
    public class MWorld : ModWorld {
		public MWorld(ModBase modbase) : base(modbase) { }

		public override void Initialize() { } //Called when the world is initialized.
		public override void PostUpdate() { } //Called after the world Update hook.
		public override bool? CheckChristmas() { return null; } //If this returns true, it acts like it is christmas. If it returns null, it uses normal code.
		public override bool? CheckHalloween() { return null; } //If this returns true, it acts like it is halloween. If it returns null, it uses normal code.
		public override void WorldGenPostInit() { } //Called just before the world generates.
		public override void WorldGenModifyTaskList(List<WorldGenTask> list) { } //Called to modify the world gen task list.
		public override void WorldGenModifyHardmodeTaskList(List<WorldGenTask> list) { } //Called to modify the hardmode world gen task list.
    }
}
