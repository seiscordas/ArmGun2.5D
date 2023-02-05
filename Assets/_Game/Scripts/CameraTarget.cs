using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kl
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private CharacterControl characterControl;
        [SerializeField] private Transform cameraTarget;
        [Range(0f, 5f)]
        [SerializeField] float cameraTargetOffsetZ = 2f;
        [Range(0.5f, 50f)]
        [SerializeField] float cameraTargetFlipSpeed = 2f;
        [Range(0f, 5f)]
        [SerializeField] float characterSpeedInfluence = 2f;

        private void FixedUpdate()
        {
            if (characterControl.Fliping)
            {
                cameraTarget.localPosition = Vector3.Lerp(Vector3.up, cameraTarget.localPosition, Time.fixedDeltaTime * cameraTargetFlipSpeed);
                return;
            }
            float currentOffsetZ = Mathf.Lerp(cameraTarget.localPosition.z, cameraTargetOffsetZ, Time.fixedDeltaTime * cameraTargetFlipSpeed);
            currentOffsetZ += Time.fixedDeltaTime * characterSpeedInfluence;
            cameraTarget.localPosition = new Vector3(cameraTarget.localPosition.x, cameraTarget.localPosition.y, currentOffsetZ);
        }
    }
}