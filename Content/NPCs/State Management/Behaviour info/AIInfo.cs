
namespace OmoriMod.Content.NPCs.State_Management.Behaviour_Info
{
    /// <summary>
    /// Manages <see cref="NPCBehaviour.ExitStatus"/>
    /// </summary>
    public class AIInfo
    {
        private int _exitStatus;

        public int ExitStatus { get => _exitStatus; set => _exitStatus = value; }

        public AIInfo()
        {
            _exitStatus = -2;
        }
    }
}
