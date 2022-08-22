using System;
using System.Collections.Generic;
using UnityEngine;

namespace kl
{
    public class CharacterControl : MonoBehaviour
    {
        public Animator Animator;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool TurnBackByRight;
        public bool TurnBackByLeft;
        public bool Turn;
        public bool isGrounded;
        
        //public bool Turn { get; internal set; }
        public bool FacingRight = true;
        [SerializeField] private GameObject ColliderEdgePrefab;

        private Rigidbody _rigidbody;
        public List<GameObject> BottomSpheres = new List<GameObject>();

        public Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                {
                    _rigidbody = GetComponent<Rigidbody>();
                }
                return _rigidbody;
            }
        }


        private void Awake()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
            float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
            float front = boxCollider.bounds.center.z + boxCollider.bounds.extents.z;
            float back = boxCollider.bounds.center.z - boxCollider.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front), this.transform);
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back), this.transform);

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);

            float sec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;

            for (int i = 0; i < 4; i++)
            {
                Vector3 position = bottomBack.transform.position + (Vector3.forward * sec * (i + 1));
                GameObject obj = CreateEdgeSphere(position, this.transform);
                BottomSpheres.Add(obj);
            }
        }
        public GameObject CreateEdgeSphere(Vector3 position, Transform parent)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, position, Quaternion.identity, parent);
            return obj;
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
        TurnBackByLeft,
        Grounded
    }
}
