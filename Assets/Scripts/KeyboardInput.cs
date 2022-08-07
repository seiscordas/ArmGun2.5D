using UnityEngine;

namespace kl
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private CharacterControl characterControl;
        void Update()
        {
            if (Input.GetAxisRaw("Horizontal") == 0 || characterControl.Turn)
            {
                VirtualInputManager.Instance.MoveRight = false;
                VirtualInputManager.Instance.MoveLeft = false;
                VirtualInputManager.Instance.TurnBackByRight = false;
                VirtualInputManager.Instance.TurnBackByLeft = false;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (characterControl.FacingRight)
                    VirtualInputManager.Instance.MoveRight = true;
                else
                    VirtualInputManager.Instance.TurnBackByLeft = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (!characterControl.FacingRight)
                    VirtualInputManager.Instance.MoveLeft = true;
                else
                    VirtualInputManager.Instance.TurnBackByRight = true;
            }
            if (Input.GetKey(KeyCode.Space) && !characterControl.Turn)
                VirtualInputManager.Instance.Jump = true;
            else
                VirtualInputManager.Instance.Jump = false;
        }
    }
}
