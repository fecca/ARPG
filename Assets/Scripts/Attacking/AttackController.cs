using System;
using System.Collections.Generic;
using ARPG.Characters;
using ARPG.Moving;
using ARPG.Zenject;
using CreatorKitCodeInternal;
using UnityEngine;
using Zenject;

namespace ARPG.Attacking
{
    public class AttackController : MonoBehaviour, AnimationControllerDispatcher.IAttackFrameReceiver
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private Animator _animator;
        [SerializeField] private AttackSkill[] _attackSkills;

        private int _attackParameterId;
        private int _attackSpeedParameterId;
        private Raycaster _raycaster;
        private RaycastHit _hitInfo;
        private AttackSkill _currentAttackSkill;
        private Projectile.Factory _projectileFactory;
        private CharacterStats _stats;

        public bool IsAttacking { get; private set; }

        [Inject]
        public void Construct(ISignalBusAdapter signalBusAdapter, Raycaster raycaster,
            Projectile.Factory projectileFactory)
        {
            _projectileFactory = projectileFactory;
            _raycaster = raycaster;
        }

        private void Awake()
        {
            _attackParameterId = Animator.StringToHash("Attack");
            _attackSpeedParameterId = Animator.StringToHash("AttackSpeed");
            _currentAttackSkill = _attackSkills[0];
        }

        public void SetStats(CharacterStats stats)
        {
            _stats = stats;
        }

        public void Attack(Vector3 mousePosition)
        {
            if (!_raycaster.RaycastGround(mousePosition, out _hitInfo)) return;

            IsAttacking = true;
            _rootTransform.LookAt(_hitInfo.point);
            _animator.SetTrigger(_attackParameterId);
            _animator.SetFloat(_attackSpeedParameterId, _stats.attackSpeed);
        }

        public void AttackFrame()
        {
            switch (_currentAttackSkill.attackType)
            {
                case AttackType.Single:
                    SpawnProjectiles(new[] { 0 });
                    break;
                case AttackType.Spread:
                    SpawnProjectiles(new[] { -30, 0, 30 });
                    break;
                case AttackType.Area:
                    SpawnProjectiles(new[] { -180, -135, -90, -45, 0, 45, 90, 135 });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            IsAttacking = false;
        }

        private void SpawnProjectiles(IEnumerable<int> angles)
        {
            foreach (var angle in angles)
            {
                var position = _attackPosition.position;
                var target = new Vector3(_hitInfo.point.x, position.y, _hitInfo.point.z);
                var direction = Quaternion.AngleAxis(angle, Vector3.up) * (target - position);

                _projectileFactory.Create(position, direction.normalized, _currentAttackSkill.range);
            }
        }

        public void SetAttackSkill(int skillIndex)
        {
            _currentAttackSkill = _attackSkills[skillIndex];
        }
    }
}