using UnityEngine;
using Cinemachine;

namespace Game.Core
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField]private CinemachineTargetGroup targetGroup;

        [Min(1)]
        [SerializeField]private float finalRadius = 4.5f;
        [SerializeField]private float zoomSpeed = 10.0f;
        [SerializeField]private Transform followTransform;
        private float currentRadius = 0.0f;

        public float FinalRadius { get => finalRadius; set => finalRadius = value; }

        private void LateUpdate() 
        {
            currentRadius = Mathf.Lerp(currentRadius, finalRadius, zoomSpeed * Time.deltaTime);
            if(followTransform != null)
            {
                targetGroup.RemoveMember(followTransform);
                targetGroup.AddMember(followTransform, 1.0f, currentRadius);
            }
        }
    }
}
