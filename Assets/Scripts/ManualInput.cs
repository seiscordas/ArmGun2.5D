using System.Collections;
using System.Collections.Generic;
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
            if (VirtualInputManager.Instance.MoveRight)
            {
                characterControl.MoveRight = true;
            }
            else
            {
                characterControl.MoveRight = false;
            }
            if (VirtualInputManager.Instance.MoveLeft)
            {
                characterControl.MoveLeft = true;
            }
            else
            {
                characterControl.MoveLeft = false;
            }
            if (VirtualInputManager.Instance.TurnBackByRight)
            {
                characterControl.TurnBackByRight = true;
            }
            else
            {
                characterControl.TurnBackByRight = false;
            }
            if (VirtualInputManager.Instance.TurnBackByLeft)
            {
                characterControl.TurnBackByLeft = true;
            }
            else
            {
                characterControl.TurnBackByLeft = false;
            }
            if (VirtualInputManager.Instance.Jump)
            {
                characterControl.Jump = true;
            }
            else
            {
                characterControl.Jump = false;
            }
        }
    }
}
