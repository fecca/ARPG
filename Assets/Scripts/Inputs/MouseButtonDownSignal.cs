using UnityEngine;

namespace ARPG.Inputs
{
	public class MouseButtonDownSignal
	{
		public Vector3 MousePosition;

		public MouseButtonDownSignal(Vector3 mousePosition)
		{
			MousePosition = mousePosition;
		}
	}
}