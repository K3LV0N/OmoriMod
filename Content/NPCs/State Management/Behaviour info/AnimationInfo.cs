using System.Collections.Generic;

namespace OmoriMod.Content.NPCs.State_Management.Behaviour_Info
{
    public class AnimationInfo
    {
        private const string DEFAULT = "default";
        private Dictionary<string, NPCAnimation> _animationInfo;
        private string _selectedFrameIterator;

        public string SelectedAnimation { get => _selectedFrameIterator; }

        public AnimationInfo(int totalFrames)
        {
            _selectedFrameIterator = DEFAULT;
            NPCAnimation frameIterator = new NPCAnimation(totalFrames-1);

            _animationInfo = [];
            _animationInfo.Add(_selectedFrameIterator, frameIterator);
        }

        public AnimationInfo(AnimationInfo previousInfo)
        {
            _animationInfo = new Dictionary<string, NPCAnimation>();
            foreach (var kvp in previousInfo._animationInfo)
            {
                _animationInfo.Add(kvp.Key, kvp.Value.Copy()); // You'll need to add a Copy() to NPCAnimation
            }
            _selectedFrameIterator = previousInfo._selectedFrameIterator;
        }

        public int GetCurrentFrame()
        {
            return _animationInfo[_selectedFrameIterator].CurrentFrame;
        }

        /// <summary>
        /// Sets the current frame. This method also switches the animation to the animation containing
        /// the frame. <see cref="DEFAULT"/> animation is chose from last. If the frame does not exist, nothing
        /// changes and false is returned.
        /// </summary>
        /// <param name="newFrame"></param>
        /// <returns>True if successful, false otherwise</returns>
        public bool SetCurrentFrame(int newFrame)
        {
            foreach(var key in _animationInfo.Keys)
            {
                var val = _animationInfo[key];
                if (key != DEFAULT && val.ContainsIndex(newFrame))
                {
                    SelectAnimation(key);
                    _animationInfo[key].CurrentFrame = newFrame;
                    return true;
                }
            }

            _selectedFrameIterator = DEFAULT;
            if (_animationInfo[_selectedFrameIterator].ContainsIndex(newFrame)) 
            {
                SelectAnimation(_selectedFrameIterator);
                _animationInfo[_selectedFrameIterator].CurrentFrame = newFrame;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Sets the current animation to loop through. Returns true if a new animation was sucessfuly selected.
        /// </summary>
        /// <param name="animationName"></param>
        /// <returns></returns>
        public bool SelectAnimation(string animationName)
        {
            
            if (_animationInfo.ContainsKey(animationName)) {
                if (_selectedFrameIterator == animationName) { return true; }
                _animationInfo[_selectedFrameIterator].Reset();
                _selectedFrameIterator = animationName;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a set of frames to <see cref="_animationInfo"/>
        /// </summary>
        /// <param name="animationName"></param>
        /// <param name="iter"></param>
        /// <returns></returns>
        public bool AddAnimation(string animationName, NPCAnimation iter)
        {
            if (_animationInfo.ContainsKey(animationName)) { return false; }
            _animationInfo.Add(animationName, iter);
            return true;
        }

        public static AnimationInfo operator ++(AnimationInfo a)
        {
            a._animationInfo[a._selectedFrameIterator]++; 
            return a;
        }

        public AnimationInfo Copy()
        {
            return new AnimationInfo(this);
        }
    }
}
