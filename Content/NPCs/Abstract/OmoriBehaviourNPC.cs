using OmoriMod.Content.NPCs.StateManagement;

namespace OmoriMod.Content.NPCs.Abstract
{
    public abstract class OmoriBehaviourNPC : OmoriNPC
    {
        protected BehaviourManager behaviourManager;

        public OmoriBehaviourNPC()
        {
            behaviourManager = new BehaviourManager();
        }
    }
}
