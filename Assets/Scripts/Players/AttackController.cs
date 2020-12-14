using System;
using System.Collections.Generic;
using ARPG.Inputs;
using ARPG.Zenject;
using Cinemachine.Utility;
using CreatorKitCodeInternal;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

namespace ARPG.Players
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

		public bool IsAttacking { get; private set; }

		[Inject]
		public void Construct(ISignalBusAdapter signalBusAdapter, Raycaster raycaster)
		{
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
					AttackSingle(_currentAttackSkill);
					break;
				case AttackType.Spread:
					AttackSpread(_currentAttackSkill);
					break;
				case AttackType.Area:
					AttackArea(_currentAttackSkill);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			IsAttacking = false;
		}

		private void AttackSingle(AttackSkill attackSkill)
		{
			SpawnProjectiles(attackSkill, new[] {0});
		}

		private void AttackSpread(AttackSkill attackSkill)
		{
			SpawnProjectiles(attackSkill, new[] {-30, 0, 30});
		}

		private void AttackArea(AttackSkill attackSkill)
		{
			SpawnProjectiles(attackSkill, new[] {-180, -135, -90, -45, 0, 45, 90, 135});
		}

		private void SpawnProjectiles(AttackSkill attackSkill, IEnumerable<int> angles)
		{
			foreach (var angle in angles)
			{
				var attackPosition = _attackPosition.position;
				var target = new Vector3(_hitInfo.point.x, attackPosition.y, _hitInfo.point.z);
				var direction = Quaternion.AngleAxis(angle, Vector3.up) * (target - attackPosition);

				var projectile = Instantiate(attackSkill.projectile, attackPosition, Quaternion.identity);
				projectile.GetComponent<Rigidbody>().AddForce(direction.normalized * attackSkill.projectileForce);
				Destroy(projectile, 2.0f);
			}
		}

		private void OnKeyUp(KeyUpSignal signal)
		{
			Debug.Log($"Switching from {_currentAttackSkill.attackType}...");
			switch (signal.KeyCode)
			{
				case KeyCode.Alpha0:
					_currentAttackSkill = _attackSkills[0];
					break;
				case KeyCode.Alpha1:
					_currentAttackSkill = _attackSkills[1];
					break;
				case KeyCode.Alpha2:
					_currentAttackSkill = _attackSkills[2];
					break;
			}

			Debug.Log($"...to {_currentAttackSkill.attackType}");
		}
	}
}