using UnityEngine;

namespace ARPG.Inputs
{
	public class MouseButtonHoldSignal
	{
		public Vector3 MousePosition;

		public MouseButtonHoldSignal(Vector3 mousePosition)
		{
			MousePosition = mousePosition;
		}
	}
}