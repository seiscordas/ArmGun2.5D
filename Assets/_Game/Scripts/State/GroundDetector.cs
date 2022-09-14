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
                animator.SetBool(TransitionParameter.Grounded.ToString(), IsGrounded(characterControl));
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
            if (characterControl.Rigidbody.velocity.y < 0f)
            {
                foreach (GameObject item in characterControl.BottomSpheres)
                {
                    Debug.DrawRay(item.transform.position, Vector3.down * Distance, Color.yellow);
                    if (Physics.Raycast(item.transform.position, Vector3.down, out RaycastHit raycastHit, Distance))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
