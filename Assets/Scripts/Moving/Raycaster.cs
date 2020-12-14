using UnityEngine;

namespace ARPG.Moving
{
	public class Raycaster
	{
		private readonly Camera _camera;
		private readonly LayerMask _groundLayer = 1 << LayerMask.NameToLayer("Level");

		public Raycaster(Camera camera)
		{
			_camera = camera;
		}

		public bool RaycastGround(Vector3 position, out RaycastHit raycastHit)
		{
			return Physics.Raycast(_camera.ScreenPointToRay(position), out raycastHit, 1000.0f, _groundLayer);
		}
	}
}