using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Util;
using System.Collections.Generic;

namespace OmoriMod.Content.NPCs.StateManagement.NPCBehaviours
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

        public int ExitStatus { get => BehaviourInfo.ExitStatus; }
        public int CurrentFrame { get => BehaviourInfo.CurrentFrame; }

        public BehaviourInfo BehaviourInfo { get => behaviourInfo; set => behaviourInfo = value; }
        protected BehaviourInfo behaviourInfo;


        public string BehaviourName { get => behaviourName; }

        protected string behaviourName;
        


        /// <summary>
        /// True for 1 tick. Indicates the method just completed.
        /// </summary>


        private void Init(string name)
        {
            behaviourParameters = new List<float>();
            _inProgress = false;
            _hasStarted = false;
            _justCompleted = false;
            behaviourInfo = new BehaviourInfo();
            behaviourName = name.OmoriModString();
        }

        /// <summary>
        /// Creates a <see cref="NPCBehaviour"/> with no name. NOTE: No name != (name = null)
        /// </summary>
        public NPCBehaviour()
        {
            Init("");
        }

        /// <summary>
        /// Creates a <see cref="NPCBehaviour"/>.
        /// </summary>
        /// <param name="name"></param>
        public NPCBehaviour(string name)
        {
            Init(name);
        }

        /// <summary>
        /// Resets the attack. NOTE: this is NOT where parameter initilization occurs. Please check <see cref="OnStart"/>
        /// </summary>
        public void Reset()
        {
            _inProgress = false;
            _hasStarted = false;
            _justCompleted = false;
            // do not destroy frame knowledge
            behaviourInfo.ExitStatus = -2;
        }

        /// <summary>
        /// Resets the attack. NOTE: this is NOT where parameter initilization occurs. Please check <see cref="OnStart"/>
        /// </summary>
        public void FullReset()
        {
            _inProgress = false;
            _hasStarted = false;
            _justCompleted = false;
            // destroy frame knowledge
            behaviourInfo = new BehaviourInfo();
        }

        /// <summary>
        /// A hook method to get parameters ready
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// A hook method to allow you to write AI. Make sure to edit <see cref="behaviourInfo"/> in this function
        /// </summary>
        /// <returns>Void</returns>
        protected virtual void AI() { }

        /// <summary>
        /// hook method to allow you to pick your frame from the NPC. Use <paramref name="frameHeight"/> to determine frame height
        /// and <see cref="CurrentFrame"/> for frame info. Set frame info in <see cref="behaviourInfo"/> in order for it to 
        /// carry over to the next <see cref="NPCBehaviour"/>
        /// </summary>
        /// <param name="frameHeight"></param>
        protected virtual void FindFrame(int frameHeight) { }

        /// <summary>
        /// hook method to set maximum frames in animation for easy frame iteration.
        /// Call <see cref="BehaviourInfo.SetMaxFrames(int)"/> inside of here.
        /// </summary>
        /// <param name="maxFrames"></param>
        protected virtual void SetMaxFrames() { }

        public void SetNPC(OmoriNPC npc)
        {
            this.npc = npc;
        }

        private void Start(OmoriNPC npc, BehaviourInfo info) {
            _inProgress = true;
            _hasStarted = true;
            behaviourInfo.ExitStatus = -1;
            behaviourInfo.CurrentFrame = info.CurrentFrame;
            SetNPC(npc);
            OnStart();
        }

        /// <summary>
        /// Call this method to use your attack
        /// </summary>
        public void PerformAI(OmoriNPC npc, BehaviourInfo info) {
            if (IsDone) {
                _justCompleted = false;
                return;
            }

            if (!_hasStarted) {
                Start(npc, info);
            }

            AI();
            if (behaviourInfo.ExitStatus >= 0) {
                _inProgress = false;
                _justCompleted = true;
            }
        }

        public void PerformFindFrame(int frameHeight)
        {
            FindFrame(frameHeight);
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
