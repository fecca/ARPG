using System;
using System.Collections.Generic;
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
		private Raycaster _raycaster;
		private RaycastHit _hitInfo;
		private AttackSkill _currentAttackSkill;
		private ISignalBusAdapter _signalBusAdapter;
		private Projectile.Factory _projectileFactory;

		public bool IsAttacking { get; private set; }

		[Inject]
		public void Construct(ISignalBusAdapter signalBusAdapter, Raycaster raycaster,
			Projectile.Factory projectileFactory)
		{
			_projectileFactory = projectileFactory;
			_signalBusAdapter = signalBusAdapter;
			_raycaster = raycaster;
		}

		private void Awake()
		{
			_attackParameterId = Animator.StringToHash("Attack");
			_currentAttackSkill = _attackSkills[0];
		}

		private void Start()
		{
			_signalBusAdapter.Subscribe<KeyUpSignal>(OnKeyUp);
		}

		public void Attack(Vector3 position)
		{
			if (!_raycaster.RaycastGround(position, out _hitInfo)) return;

			IsAttacking = true;
			_rootTransform.LookAt(_hitInfo.point);
			_animator.SetTrigger(_attackParameterId);
		}

		public void AttackFrame()
		{
			switch (_currentAttackSkill.attackType)
			{
				case AttackType.Single:
					SpawnProjectiles(new[] {0});
					break;
				case AttackType.Spread:
					SpawnProjectiles(new[] {-30, 0, 30});
					break;
				case AttackType.Area:
					SpawnProjectiles(new[] {-180, -135, -90, -45, 0, 45, 90, 135});
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

		private void OnKeyUp(KeyUpSignal signal)
		{
			switch (signal.KeyCode)
			{
				case KeyCode.Alpha1:
					_currentAttackSkill = _attackSkills[0];
					break;
				case KeyCode.Alpha2:
					_currentAttackSkill = _attackSkills[1];
					break;
				case KeyCode.Alpha3:
					_currentAttackSkill = _attackSkills[2];
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}