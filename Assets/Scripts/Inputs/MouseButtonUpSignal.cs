using UnityEngine;

namespace ARPG.Inputs
{
	public class MouseButtonUpSignal
	{
		public Vector3 MousePosition;

		public MouseButtonUpSignal(Vector3 mousePosition)
		{
			MousePosition = mousePosition;
		}
	}
}