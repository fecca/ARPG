using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private float _lmbHoldSignalInterval = 0.1f;

        private ISignalBusAdapter _signalBusAdapter;
        private float _holdSignalTimer;

        [Inject]
        public void Construct(ISignalBusAdapter signalBusAdapter)
        {
            _signalBusAdapter = signalBusAdapter;
        }

        private void Update()
        {
            CheckMouseButtonDown(0);
            CheckMouseButtonHold(0);
            CheckMouseButtonUp(0);

            CheckMouseButtonDown(1);
            CheckMouseButtonHold(1);
            CheckMouseButtonUp(1);
        }

        private void CheckMouseButtonDown(int button)
        {
            if (!Input.GetMouseButtonDown(button)) return;

            _signalBusAdapter.Fire(new MouseButtonDownSignal(Input.mousePosition, button));
        }

        private void CheckMouseButtonHold(int button)
        {
            if (!Input.GetMouseButton(button)) return;

            if (_holdSignalTimer < _lmbHoldSignalInterval)
            {
                _holdSignalTimer += Time.deltaTime;
                return;
            }

            _signalBusAdapter.Fire(new MouseButtonHoldSignal(Input.mousePosition, button));
            _holdSignalTimer = 0f;
        }

        private void CheckMouseButtonUp(int button)
        {
            if (!Input.GetMouseButtonUp(button)) return;

            _signalBusAdapter.Fire(new MouseButtonUpSignal(Input.mousePosition, button));
        }
    }
}