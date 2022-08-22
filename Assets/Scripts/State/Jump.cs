using UnityEngine;
namespace kl
{
    [CreateAssetMenu(fileName = "New State", menuName = "KL/AbilityData/Jump")]
    public class Jump : StateData
    {
        public float JumpForce;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.GetCharacterControl(animator).Rigidbody.AddForce(Vector3.up * JumpForce);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
