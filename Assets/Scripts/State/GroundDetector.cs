using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace kl
{
    [CreateAssetMenu(fileName = "New State", menuName = "KL/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        [Range(0f, 1f)]
        public float CheckTime;
        public float Distance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= CheckTime)
            {
                CharacterControl characterControl = characterState.GetCharacterControl(animator);
                if (IsGrounded(characterControl))
                {
                    animator.SetBool(TransitionParameter.Grounded.ToString(), true);
                }
                else
                {
                    animator.SetBool(TransitionParameter.Grounded.ToString(), false);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        bool IsGrounded(CharacterControl characterControl)
        {
            if (characterControl.Rigidbody.velocity.y > -0.01f && characterControl.Rigidbody.velocity.y <= 0f)
            {
                return true;
            }
            foreach (GameObject item in characterControl.BottomSpheres)
            {
                Debug.DrawRay(item.transform.position, Vector3.down * 0.7f, Color.yellow);
                RaycastHit raycastHit;
                if (Physics.Raycast(item.transform.position, Vector3.down, out raycastHit, Distance))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
