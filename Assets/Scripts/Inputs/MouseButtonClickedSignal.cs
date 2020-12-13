using UnityEngine;

namespace ARPG.Inputs
{
	public class MouseButtonClickedSignal
	{
		public Vector3 MousePosition;

		public MouseButtonClickedSignal(Vector3 mousePosition)
		{
			MousePosition = mousePosition;
		}
	}
}