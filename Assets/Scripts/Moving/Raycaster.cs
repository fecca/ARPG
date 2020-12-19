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
			var ray = _camera.ScreenPointToRay(position);
			var raycast = Physics.Raycast(ray, out raycastHit, 1000.0f, _groundLayer);

			Debug.DrawRay(ray.origin, raycastHit.point, Color.magenta, 100f);

			return raycast;
		}
	}
}