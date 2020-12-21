using UnityEngine;

namespace ARPG.Characters
{
	[ExecuteInEditMode]
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera playerCamera;
		[SerializeField] private CameraConfig cameraConfig;

		private void Awake()
		{
			playerCamera.transform.localRotation = Quaternion.Euler(cameraConfig.localRotation);
		}

		private void Update()
		{
			playerCamera.transform.position = transform.position + cameraConfig.localPosition;
			playerCamera.transform.rotation = Quaternion.Euler(cameraConfig.localRotation);
		}
	}
}