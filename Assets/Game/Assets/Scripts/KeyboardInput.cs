using UnityEngine;

namespace kl
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private CharacterControl characterControl;
        void Update()
        {            
            if (Input.GetAxisRaw("Horizontal") == 0 || characterControl.Fliping)
            {
                VirtualInputManager.Instance.MoveRight = false;
                VirtualInputManager.Instance.MoveLeft = false;
                VirtualInputManager.Instance.MoveBackward = false;
                if (characterControl.Fliping)
                {
                    return;
                }
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (characterControl.FacingRight)
                {
                    VirtualInputManager.Instance.MoveRight = true;
                }
                else if (!characterControl.FacingRight && characterControl.ActiveAim)
                {
                    VirtualInputManager.Instance.MoveBackward = true;
                }
                else
                {
                    VirtualInputManager.Instance.FlipToRight = true;
                    characterControl.Fliping = true;
                }
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (!characterControl.FacingRight)
                {
                    VirtualInputManager.Instance.MoveLeft = true;
                }
                else if (characterControl.FacingRight && characterControl.ActiveAim)
                {
                    VirtualInputManager.Instance.MoveBackward = true;
                }
                else
                {
                    VirtualInputManager.Instance.FlipToLeft = true;
                    characterControl.Fliping = true;
                }
            }
            VirtualInputManager.Instance.Jump = (Input.GetKey(KeyCode.Space) && !characterControl.Fliping);

        }
    }
}
