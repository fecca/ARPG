using UnityEngine;
using Zenject;

namespace ARPG.Characters
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private float zoomDistance = 10.0f;
		[SerializeField] private Transform follower;

		private Camera _playerCamera;

		[Inject]
		public void Construct(Camera cam)
		{
			_playerCamera = cam;
		}

		private void Update()
		{
			var followerPosition = follower.position;
			var cameraTransform = _playerCamera.transform;
			cameraTransform.position = new Vector3(followerPosition.x, cameraTransform.position.y,
				followerPosition.z - zoomDistance);
		}
	}
}