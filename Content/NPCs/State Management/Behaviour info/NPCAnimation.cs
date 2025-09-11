
namespace OmoriMod.Content.NPCs.State_Management.Behaviour_Info
{
    public class NPCAnimation
    {
        private readonly int _beginningIndex;
        private readonly int _endingIndex;

        public int CurrentFrame { get => _currentIndex; set => _currentIndex = value; }
        private int _currentIndex;

        public NPCAnimation(int begin, int end) {
            _beginningIndex = begin;
            _currentIndex = begin;
            _endingIndex = end;
        }

        public NPCAnimation(int end) {
            _beginningIndex = 0;
            _currentIndex = 0;
            _endingIndex = end;     
        }

        public NPCAnimation(NPCAnimation iter)
        {
            _beginningIndex = iter._beginningIndex;
            _currentIndex = iter._currentIndex;
            _endingIndex = iter._endingIndex;
        }

        public void Reset()
        {
            _currentIndex = _beginningIndex;
        }

        public bool ContainsIndex(int index)
        {
            return _beginningIndex <= index ||  index <= _endingIndex;
        }

        public NPCAnimation Copy()
        {
            return new NPCAnimation(this);
        }


        public static NPCAnimation operator ++(NPCAnimation f)
        {
            if (f._currentIndex < f._endingIndex)
            {
                f._currentIndex++;
            }
            else
            {
                f._currentIndex = f._beginningIndex;
            }
            return f;
        }
    }
}
