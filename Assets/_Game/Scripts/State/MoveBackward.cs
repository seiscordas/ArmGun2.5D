using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kl
{
    [CreateAssetMenu(fileName = "New State", menuName = "KL/AbilityData/MoveBackward")]
    public class MoveBackward : StateData
    {
        public AnimationCurve SpeedGraph;
        public float Speed;
        public float BlockDistance;
        public bool Self;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl characterControl = characterState.GetCharacterControl(animator);
            if (characterControl.Fliping)
            {
                animator.SetBool(TransitionParameter.MoveBackward.ToString(), false);
            }
            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }
            if (characterControl.MoveBackward)
            {
                characterControl.transform.Translate(Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.fixedDeltaTime * -Vector3.forward);
                //TODO: CHECK BACK OU COLOCAR AS ESFERAS NAS COSTAS QUANDO TIVER ANDANDO DE COSTAS OU CRIAR NOVAS (ACHO MELHOR COLOCAR A EXISTENTES NAS COSTAS)
                if (!CheckFront(characterControl))
                {
                }
            }
            else
            {
                animator.SetBool(TransitionParameter.MoveBackward.ToString(), value: false);
            }
            characterControl.transform.position = new(characterControl.transform.position.x, characterControl.transform.position.y, 0f);

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            VirtualInputManager.Instance.MoveBackward = false;
        }
        bool CheckFront(CharacterControl characterControl)
        {
            foreach (GameObject item in characterControl.FrontSpheres)
            {
                Self = false;

                Debug.DrawRay(item.transform.position, characterControl.transform.forward * BlockDistance, Color.yellow);
                if (Physics.Raycast(item.transform.position, characterControl.transform.forward, out RaycastHit raycastHit, BlockDistance))
                {
                    foreach (Collider c in characterControl.RagdollParts)
                    {
                        if (c.gameObject == raycastHit.collider.gameObject)
                        {
                            Self = true;
                            break;
                        }
                    }
                    if (!Self)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}