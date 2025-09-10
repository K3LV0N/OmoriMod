using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Util;
using System.Collections.Generic;

namespace OmoriMod.Content.NPCs.State_Management
{
    /// <summary>
    /// Helper class that can store behaviours for NPCs for state management
    /// </summary>
    public abstract class NPCBehaviour
    {

        protected List<float> behaviourParameters;

        public bool IsDone { get => !_inProgress && _hasStarted; }
        private bool _inProgress;
        private bool _hasStarted;
        public bool JustCompleted { get => _justCompleted; }
        private bool _justCompleted;

        public string BehaviourName { get => behaviourName; }
        protected string behaviourName;
        


        /// <summary>
        /// True for 1 tick. Indicates the method just completed.
        /// </summary>


        private void Init(string name)
        {
            behaviourParameters = [];
            _inProgress = false;
            _hasStarted = false;
            _justCompleted = false;
            behaviourName = name.OmoriModString();
        }

        /// <summary>
        /// Creates a <see cref="NPCBehaviour"/> with no name. NOTE: No name != (name = null)
        /// </summary>
        public NPCBehaviour()
        {
            string name = (behaviourName ?? GetType().Name).OmoriModString();
            Init(name);
        }

        /// <summary>
        /// Creates a <see cref="NPCBehaviour"/>.
        /// </summary>
        /// <param name="behaviourName">In case of special naming conventions. Typically do not pass in.</param>
        public NPCBehaviour(string behaviourName)
        {
            Init(behaviourName);
        }

        /// <summary>
        /// Resets the attack. NOTE: this is NOT where parameter initilization occurs. Please check <see cref="OnStart"/>
        /// </summary>
        public void Reset()
        {
            _inProgress = false;
            _hasStarted = false;
            _justCompleted = false;
        }

        /// <summary>
        /// A hook method to get parameters ready.
        /// </summary>
        protected virtual void OnStart(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo) { }

        /// <summary>
        /// A hook method to allow you to write AI. Make sure to edit <paramref name="behaviourInfo"/> in this function
        /// </summary>
        /// <returns>Void</returns>
        protected virtual void AI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo) { }

        /// <summary>
        /// hook method to allow you to pick your frame from the NPC. Use <paramref name="frameHeight"/> to determine frame height
        /// and <see cref="CurrentFrame"/> for frame info. Set frame info in <see cref="behaviourInfo"/> in order for it to 
        /// carry over to the next <see cref="NPCBehaviour"/>
        /// </summary>
        /// <param name="frameHeight"></param>
        protected virtual void FindFrame(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo, int frameHeight) { }


        private void Start(OmoriBehaviourNPC npc, BehaviourInfo info) {
            _inProgress = true;
            _hasStarted = true;
            info.ExitStatus = -1;
            OnStart(npc, info);
        }

        /// <summary>
        /// Call this method to use your attack
        /// </summary>
        public void PerformAI(OmoriBehaviourNPC npc, BehaviourInfo info) {
            if (IsDone) {
                _justCompleted = false;
                return;
            }

            if (!_hasStarted) {
                Start(npc, info);
            }

            AI(npc, info);
            if (info.ExitStatus >= 0) {
                _inProgress = false;
                _justCompleted = true;
            }
        }

        public void PerformFindFrame(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo,int frameHeight)
        {
            FindFrame(npc, behaviourInfo, frameHeight);
        }

        public override bool Equals(object obj)
        {
            if (obj is NPCBehaviour other)
                return BehaviourName == other.BehaviourName;
            return false;
        }

        public override int GetHashCode()
        {
            return BehaviourName.GetHashCode();
        }
    }
}
