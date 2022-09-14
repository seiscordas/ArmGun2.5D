using System;
using System.Collections;
using UnityEngine;

namespace kl
{
    public class FlipSystem : MonoBehaviour
    {
        [SerializeField] private CharacterControl characterControl;
        [SerializeField] private float _flipAngle = 0f;
        [SerializeField] private float _flipSpeed = 30f;

        private void FixedUpdate()
        {
            if (characterControl.Fliping)
            {
                Flip(_flipSpeed);
            }
        }
        private void Flip(float speed)
        {
            if (characterControl.FlipToLeft)
            {
                _flipAngle -= speed;
            }
            if (characterControl.FlipToRight)
            {
                _flipAngle += speed;
                speed *= -1;
            }

            _flipAngle = Mathf.Clamp(_flipAngle, -90, 90);
            transform.Rotate(0, speed, 0);

            if (_flipAngle == 90f || _flipAngle == -90f)
            {
                characterControl.Fliping = false;
                VirtualInputManager.Instance.FlipToLeft = false;
                VirtualInputManager.Instance.FlipToRight = false;
                characterControl.FacingRight = (_flipAngle == 90f);
                characterControl.transform.rotation = Quaternion.Euler(0f, _flipAngle, 0f);
            }
            transform.position = new(transform.position.x, transform.position.y, 0f);
        }
    }
}