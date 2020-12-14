using UnityEngine;

namespace ARPG.Inputs
{
    public class MouseButtonDownSignal
	{
		public Vector3 MousePosition;
        public int Button;

        public MouseButtonDownSignal(Vector3 mousePosition, int button)
        {
            Button = button;
            MousePosition = mousePosition;
        }
	}
}