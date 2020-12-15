using System;
using ARPG.Attacking;
using ARPG.Inputs;
using ARPG.Moving;
using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Characters
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AttackController _attackController;
        [SerializeField] private CharacterStats _stats;

        private ISignalBusAdapter _signalBusAdapter;

        [Inject]
        public void Construct(ISignalBusAdapter signalBusAdapter)
        {
            _signalBusAdapter = signalBusAdapter;
        }

        private void Start()
        {
            _signalBusAdapter.Subscribe<MouseButtonHoldSignal>(OnMouseButtonHold);
            _signalBusAdapter.Subscribe<MouseButtonUpSignal>(OnMouseButtonUp);
            _signalBusAdapter.Subscribe<KeyUpSignal>(OnKeyUp);
        }

        private void OnMouseButtonHold(MouseButtonHoldSignal signal)
        {
            if (_attackController.IsAttacking) return;

            switch (signal.Button)
            {
                case 0:
                    Move(signal.MousePosition);
                    break;
                case 1:
                    Attack(signal.MousePosition);
                    break;
            }
        }

        private void OnMouseButtonUp(MouseButtonUpSignal signal)
        {
            if (_attackController.IsAttacking) return;

            switch (signal.Button)
            {
                case 0:
                    Move(signal.MousePosition);
                    break;
                case 1:
                    break;
            }
        }

        private void OnKeyUp(KeyUpSignal signal)
        {
            switch (signal.KeyCode)
            {
                case KeyCode.Alpha1:
                    _attackController.SetAttackSkill(0);
                    break;
                case KeyCode.Alpha2:
                    _attackController.SetAttackSkill(1);
                    break;
                case KeyCode.Alpha3:
                    _attackController.SetAttackSkill(2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Move(Vector3 mousePosition)
        {
            _movementController.Move(mousePosition, _stats.movementSpeed);
        }

        private void Attack(Vector3 mousePosition)
        {
            _movementController.StopMoving();
            _attackController.Attack(mousePosition, _stats.attackSpeed);
        }
    }
}