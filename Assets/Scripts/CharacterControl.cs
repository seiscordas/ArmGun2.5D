using System;
using UnityEngine;

namespace kl
{
    public class CharacterControl : MonoBehaviour
    {
        public float Speed;
        public Animator Animator;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool TurnBackByRight;
        public bool TurnBackByLeft;
        public bool Turn;
        //public bool Turn { get; internal set; }

        public bool FacingRight = true;


        private readonly Material material;

        public void ChangeMaterial()
        {
            if (material == null)
            {
                Debug.LogError("No material specified");
            }
            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (var r in arrMaterials)
            {
                if (r.gameObject != this.gameObject)
                {
                    r.material = material;
                }
            }
        }
    }
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        MoveLeft,
        MoveRight,
        TurnLeft,
        TurnRight,
        TurnBackByRight,
        TurnBackByLeft
    }
}
