using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kl
{
    [CreateAssetMenu(fileName = "New State", menuName = "KL/AbilityData/Moveforward")]
    public class MoveForward : StateData
    {
        public float Speed;
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
            if (characterControl.MoveRight && characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }
            if (!characterControl.MoveRight && !characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }
            characterControl.transform.position = new Vector3(characterControl.transform.position.x, characterControl.transform.position.y, 0f);
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
