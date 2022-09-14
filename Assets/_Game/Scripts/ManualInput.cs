using UnityEngine;

namespace kl
{
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl characterControl;

        private void Awake()
        {
            characterControl = this.GetComponent<CharacterControl>();
        }

        private void Update()
        {
            characterControl.MoveRight = VirtualInputManager.Instance.MoveRight;
            characterControl.MoveLeft = VirtualInputManager.Instance.MoveLeft;
            characterControl.FlipToRight = VirtualInputManager.Instance.FlipToRight;
            characterControl.FlipToLeft = VirtualInputManager.Instance.FlipToLeft;
            characterControl.Jump = VirtualInputManager.Instance.Jump;
            characterControl.ActiveAim = VirtualInputManager.Instance.ActiveAim;
            characterControl.Grounded = VirtualInputManager.Instance.Grounded;
            characterControl.MoveBackward = VirtualInputManager.Instance.MoveBackward;
        }
    }
}
