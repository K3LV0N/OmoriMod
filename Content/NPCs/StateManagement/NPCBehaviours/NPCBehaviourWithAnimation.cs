namespace OmoriMod.Content.NPCs.StateManagement.NPCBehaviours
{
    public class NPCBehaviourWithAnimation : NPCBehaviour
    {
        protected readonly int _maxFrames;

        public NPCBehaviourWithAnimation(string name, int maxFrames) : base(name)
        {
            _maxFrames = maxFrames;
            behaviourInfo.SetMaxFrames(_maxFrames);
        }
    }
}
