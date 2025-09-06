namespace OmoriMod.Content.NPCs.StateManagement
{
    public class BehaviourInfo
    {
        private int _exitStatus;
        private int _currentFrame;
        private int _maxFrames;

        public int ExitStatus { get => _exitStatus; set => _exitStatus = value; }
        public int CurrentFrame { get => _currentFrame; set => _currentFrame = value; }

        public BehaviourInfo() {
            _exitStatus = -2;
            _currentFrame = 0;
            _maxFrames = -1;
        }

        public BehaviourInfo(int exitStatus, int currentFrame) { 
            _exitStatus = exitStatus;
            _currentFrame = currentFrame;
            _maxFrames = -1;
        }


        public void SetMaxFrames(int maxFrames)
        {
            _maxFrames = maxFrames;
        }

        public static BehaviourInfo operator ++(BehaviourInfo b)
        {

            if(b._currentFrame < b._maxFrames - 1)
            {
                b._currentFrame++;
            }
            else
            {
                b._currentFrame = 0;
            }
                return b;
        }
    }
}
