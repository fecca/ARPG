using UnityEngine;

namespace ARPG.Attacking
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