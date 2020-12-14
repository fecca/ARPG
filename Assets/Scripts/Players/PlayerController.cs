using ARPG.Inputs;
using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private AttackController _attackController;

        private ISignalBusAdapter _signalBusAdapter;

        [Inject]
        public void Construct(ISignalBusAdapter signalBusAdapter)
        {
            _signalBusAdapter = signalBusAdapter;
        }

        private void Start()
        {
            _signalBusAdapter.Subscribe<MouseButtonDownSignal>(OnMouseButtonDown);
            _signalBusAdapter.Subscribe<MouseButtonHoldSignal>(OnMouseButtonHold);
            _signalBusAdapter.Subscribe<MouseButtonUpSignal>(OnMouseButtonUp);
        }

        private void OnMouseButtonDown(MouseButtonDownSignal signal)
        {
            switch (signal.Button)
            {
                case 0:
                    _movementController.Move(signal.MousePosition);
                    break;
                case 1:
                    _movementController.StopMoving();
                    _attackController.Attack(signal.MousePosition);
                    break;
            }
        }

        private void OnMouseButtonHold(MouseButtonHoldSignal signal)
        {
            if (_attackController.IsAttacking) return;

            switch (signal.Button)
            {
                case 0:
                    _movementController.Move(signal.MousePosition);
                    break;
                case 1:
                    _movementController.StopMoving();
                    _attackController.Attack(signal.MousePosition);
                    break;
            }
        }

        private void OnMouseButtonUp(MouseButtonUpSignal signal)
        {
            switch (signal.Button)
            {
                case 0:
                    _movementController.Move(signal.MousePosition);
                    break;
                case 1:
                    break;
            }
        }
    }
}