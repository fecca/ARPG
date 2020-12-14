using UnityEngine;

namespace ARPG.Inputs
{
    public class KeyUpSignal
    {
        public KeyCode KeyCode;

        public KeyUpSignal(KeyCode keyCode)
        {
            KeyCode = keyCode;
        }
    }
}