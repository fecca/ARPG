using UnityEngine;
using Zenject;

namespace ARPG.Characters
{
    public class CameraController : MonoBehaviour
    {
        private Camera _camera;
        private Transform _follower;

        [Inject]
        public void Construct(Camera camera)
        {
            _camera = camera;
        }

        private void Update()
        {
            var followerPosition = _follower.position;
            var cameraTransform = _camera.transform;
            cameraTransform.position = new Vector3(followerPosition.x, cameraTransform.position.y, followerPosition.z - 10f);
        }

        public void SetFollower(Transform follower)
        {
            _follower = follower;
        }
    }
}