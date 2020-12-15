using UnityEngine;
using Zenject;

namespace ARPG.Characters
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float zoomDistance = 10.0f;
        [SerializeField] private Transform follower;

        private Camera _camera;

        [Inject]
        public void Construct(Camera cam)
        {
            _camera = cam;
        }

        private void Update()
        {
            var followerPosition = follower.position;
            var cameraTransform = _camera.transform;
            cameraTransform.position = new Vector3(followerPosition.x, cameraTransform.position.y, followerPosition.z - zoomDistance);
        }
    }
}