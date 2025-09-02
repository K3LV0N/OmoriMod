using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Util;
using System.Collections.Generic;
using Terraria;

namespace OmoriMod.Content.NPCs.StateManagement
{
    /// <summary>
    /// Helper class that can store behaviours for NPCs for state management
    /// </summary>
    public abstract class NPCBehaviour
    {
        protected List<float> behaviourParameters;

        protected OmoriNPC npc;

        public bool IsDone { get => !_inProgress && _hasStarted; }
        private bool _inProgress;
        private bool _hasStarted;
        public bool JustCompleted { get => _justCompleted; }
        private bool _justCompleted;

        public int ExitStatus { get => _exitStatus; }
        private int _exitStatus;

        public string BehaviourName { get => behaviourName; }
        protected string behaviourName;
        


        /// <summary>
        /// True for 1 tick. Indicates the method just completed.
        /// </summary>


        private void Init()
        {
            behaviourParameters = new List<float>();
            _inProgress = false;
            _hasStarted = false;
            _justCompleted = false;
            _exitStatus = -2;
        }

        /// <summary>
        /// Creates a <see cref="NPCBehaviour"/> with no name. NOTE: No name != (name = null)
        /// </summary>
        public NPCBehaviour()
        {
            Init();
            behaviourName = OmoriString.str("");
        }

        /// <summary>
        /// Creates a <see cref="NPCBehaviour"/>.
        /// </summary>
        /// <param name="name"></param>
        public NPCBehaviour(string name)
        {
            Init();
            behaviourName = OmoriString.str(name);
        }

        /// <summary>
        /// Resets the attack. NOTE: this is NOT where parameter initilization occurs. Please check <see cref="OnStart"/>
        /// </summary>
        public void Reset()
        {
            _inProgress = false;
            _hasStarted = false;
            _justCompleted = false;
            _exitStatus = -2;
        }

        /// <summary>
        /// A hook method to get parameters ready
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// A hook method to allow you to write AI
        /// </summary>
        /// <returns>True when the attack is completed</returns>
        protected virtual int AI()
        {
            return -1;
        }

        private void Start(OmoriNPC npc) {
            _inProgress = true;
            _hasStarted = true;
            this.npc = npc;
            OnStart();
        }

        /// <summary>
        /// Call this method to use your attack
        /// </summary>
        public void PerformAI(OmoriNPC npc) {
            if (IsDone) {
                _justCompleted = false;
                return;
            }

            if (!_hasStarted)
            {
                Start(npc);
            }

            _exitStatus = AI();
            if (_exitStatus != -1) {
                _inProgress = false;
                _justCompleted = true;
            }
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
