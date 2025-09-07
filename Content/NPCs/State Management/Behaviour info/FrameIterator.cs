
namespace OmoriMod.Content.NPCs.State_Management.Behaviour_Info
{
    public class FrameIterator
    {
        private readonly int _beginningIndex;
        private readonly int _endingIndex;

        public int CurrentFrame { get => _currentIndex; set => _currentIndex = value; }
        private int _currentIndex;

        public FrameIterator(int begin, int end) {
            _beginningIndex = begin;
            _currentIndex = begin;
            _endingIndex = end;
        }

        public FrameIterator(int end) {
            _beginningIndex = 0;
            _currentIndex = 0;
            _endingIndex = end;     
        }

        public void Reset()
        {
            _currentIndex = _beginningIndex;
        }

        public bool ContainsIndex(int index)
        {
            return _beginningIndex <= index ||  index <= _endingIndex;
        }


        public static FrameIterator operator ++(FrameIterator f)
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
