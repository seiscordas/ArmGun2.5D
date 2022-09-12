using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kl
{
    public class CharacterControl : MonoBehaviour
    {
        public Animator Animator;
        public bool MoveBackward;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool FlipToLeft;
        public bool FlipToRight;
        public bool Grounded;
        public bool Fliping;
        public bool ActiveAim;
        public Transform AimPosition;

        public float GravityMutiplier;
        public float PullMutiplier;

        public bool FacingRight = true;
        [SerializeField] private GameObject ColliderEdgePrefab;

        private Rigidbody _rigidbody;
        public List<GameObject> BottomSpheres = new();
        public List<GameObject> FrontSpheres = new();
        public List<Collider> RagdollParts = new();

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
            SetRagdollParts();
            SetColliderSpheres();
        }

        /*
        private IEnumerator Start()
        {
            print("teste");
            yield return new WaitForSeconds(5f);
            Rigidbody.AddForce(200f * Vector3.up);
            yield return new WaitForSeconds(0.5f);
            TurnOnRagdoll();
        }
         */

        private void SetRagdollParts()
        {
            Collider[] Colliders = GetComponentsInChildren<Collider>();

            foreach (Collider c in Colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    RagdollParts.Add(c);
                }
            }
        }

        public void TurnOnRagdoll()
        {
            Rigidbody.useGravity = false;
            Rigidbody.velocity = Vector3.zero;
            Animator.enabled = false;
            Animator.avatar = null;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            foreach (Collider c in RagdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        private void SetColliderSpheres()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();

            float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
            float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
            float front = boxCollider.bounds.center.x + boxCollider.bounds.extents.x;
            float back = boxCollider.bounds.center.x - boxCollider.bounds.extents.x;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(front, bottom, 0f), this.transform);
            GameObject bottomBack = CreateEdgeSphere(new Vector3(back, bottom, 0f), this.transform);
            GameObject topFront = CreateEdgeSphere(new Vector3(front, top, 0f), this.transform);

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);
            FrontSpheres.Add(bottomFront);
            FrontSpheres.Add(topFront);

            float horizontalSection = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;
            CreateMiddleSpheres(bottomFront, -this.transform.forward, horizontalSection, 4, BottomSpheres);

            float verticalSection = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f;
            CreateMiddleSpheres(bottomFront, this.transform.up, verticalSection, 9, FrontSpheres);
        }

        public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec, int interactions, List<GameObject> spheresList)
        {
            for (int i = 0; i < interactions; i++)
            {
                Vector3 position = start.transform.position + ((i + 1) * sec * dir);
                GameObject obj = CreateEdgeSphere(position, this.transform);
                spheresList.Add(obj);
            }
        }
        public GameObject CreateEdgeSphere(Vector3 position, Transform parent)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, position, Quaternion.identity, parent);
            return obj;
        }
        private void FixedUpdate()
        {
            if (Rigidbody.velocity.y < 0f)
            {
                Rigidbody.velocity += Vector3.down * GravityMutiplier;
            }
            if (Rigidbody.velocity.x > 0f && !Jump)
            {
                Rigidbody.velocity += (Vector3.down * PullMutiplier);
            }
        }
    }
    public enum TransitionParameter
    {
        Move,
        MoveBackward,
        Jump,
        ForceTransition,
        ActiveAim,
        Grounded
    }
}
//TODO:
//colocar mira
//corrigi raycast quando atira em movimento melhor trocar por raio laser com object
//trocar animações de pulo para animação sem skin
//agachar
//CORRIGIR RESOLUÇÃO DO GAME PLAY
//Procurar animações melhores