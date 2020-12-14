using UnityEngine;

namespace ARPG.Inputs
{
    public class MouseButtonHoldSignal
    {
        public Vector3 MousePosition;
        public int Button;

        public MouseButtonHoldSignal(Vector3 mousePosition, int button)
        {
            Button = button;
            MousePosition = mousePosition;
        }
    }
}