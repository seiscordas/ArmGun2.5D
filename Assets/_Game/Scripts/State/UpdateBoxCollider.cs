using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace kl
{
    [CreateAssetMenu(fileName = "New State UpdateBoxCollider", menuName = "KL/AbilityData/UpdateBoxCollider")]
    public class UpdateBoxCollider : StateData
    {
        [Header("Center Box Collider")]
        [SerializeField] private Vector3 TargetCenter;
        [SerializeField] private float CenterUpdateSpeed;

        [Header("Size Box Collider")]
        [SerializeField] private Vector3 TargetSize;
        [SerializeField] private float SizeUpdateSpeed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl characterControl = characterState.GetCharacterControl(animator);
            characterControl.UpdateBoxColliderCenter(TargetCenter, CenterUpdateSpeed);
            characterControl.UpdateBoxColliserSize(TargetSize, SizeUpdateSpeed);
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
