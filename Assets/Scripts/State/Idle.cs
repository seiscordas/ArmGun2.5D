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
            if (characterControl.Turn)
            {
                return;
            }
            if (characterControl.MoveRight || characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
            if (characterControl.TurnBackByRight)
            {
                animator.SetBool(TransitionParameter.TurnBackByRight.ToString(), true);
            }
            if (characterControl.TurnBackByLeft)
            {
                animator.SetBool(TransitionParameter.TurnBackByLeft.ToString(), true);
            }
            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }
            else
                animator.SetBool(TransitionParameter.Jump.ToString(), false);
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
