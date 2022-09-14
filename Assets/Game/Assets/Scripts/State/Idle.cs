using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace kl
{
    [CreateAssetMenu(fileName = "New State", menuName = "KL/AbilityData/Idle")]
    public class Idle : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterControl characterControl = characterState.GetCharacterControl(animator);
            if (characterControl.Fliping)
                return;

            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), value: true);
            }
            if (characterControl.MoveBackward)
            {
                animator.SetBool(TransitionParameter.MoveBackward.ToString(), value: true);
            }
            if (characterControl.MoveRight || characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), value: true);
            }
            VirtualInputManager.Instance.Grounded = true;
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
