using UnityEngine;

namespace ARPG.Inputs
{
	public class MouseButtonUpSignal
	{
		public Vector3 MousePosition;
        public int Button;

        public MouseButtonUpSignal(Vector3 mousePosition, int button)
        {
            Button = button;
            MousePosition = mousePosition;
        }
	}
}