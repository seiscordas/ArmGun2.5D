using UnityEngine;

namespace kl
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool MoveRight { get; internal set; }
        public bool MoveLeft { get; internal set; }
        public bool FlipToLeft { get; internal set; }
        public bool FlipToRight { get; internal set; }
        public bool Jump { get; internal set; }
        public bool ActiveAim { get; internal set; }
        public bool Grounded { get; internal set; }
        public bool MoveBackward { get; internal set; }
    }
}
