﻿using OmoriMod.Content.NPCs.State_Management;

namespace OmoriMod.Content.NPCs.Classes
{
	/// <summary>
	/// Base class for NPCs that utilize the <see cref="BehaviourManager"/> system for AI behaviour and animation control.
	/// <para>
	/// Inherits from <see cref="OmoriModNPC"/> and provides a protected <see cref="BehaviourManager"/> instance, 
	/// allowing derived NPCs to define, manage, and execute multiple behaviours, background behaviours, and animations.
	/// </para>
	/// <para>
	/// Derived classes can use <see cref="behaviourManager"/> to integrate AI logic and animation updates into the NPC's update loop.
	/// </para>
	/// </summary>
	public abstract class OmoriBehaviourNPC : OmoriModNPC
    {
		/// <summary>
		/// The behaviour manager for this <see cref="OmoriBehaviourNPC"/>
		/// </summary>
        protected BehaviourManager behaviourManager;
    }
}
