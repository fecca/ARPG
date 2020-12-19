using System;
using ARPG.Attacking;
using ARPG.Items;
using ARPG.Moving;
using ARPG.Zenject;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ARPG.Characters
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private MovementController _movementController;
		[SerializeField] private LootTableConfig lootTableConfig;
		[SerializeField] private SphereTrigger hitTrigger;
		[SerializeField] private SphereTrigger aggroTrigger;

		private int _faintParameterId;
		private ISignalBusAdapter _signalBusAdapter;

		[Inject]
		private void Construct(ISignalBusAdapter signalBusAdapter)
		{
			_signalBusAdapter = signalBusAdapter;
		}

		private void Awake()
		{
			_faintParameterId = Animator.StringToHash("Death");
		}

		private void Start()
		{
			hitTrigger.Initialize(1.0f, OnHitTrigger);
			aggroTrigger.Initialize(10.0f, OnAggroTrigger);
		}

		private void OnHitTrigger(Collider other)
		{
			var projectile = other.GetComponent<Projectile>();
			if (projectile == null) return;

			TakeDamage();
		}

		private void TakeDamage()
		{
			Kill();
		}

		public void Kill()
		{
			_movementController.DisableAgent();
			_animator.SetTrigger(_faintParameterId);
			_signalBusAdapter.Fire(new EnemyDeathSignal(transform.position, lootTableConfig));
		}

		private void OnAggroTrigger(Collider other)
		{
			if (!other.CompareTag("Player")) return;

			Aggro(other.transform);
		}

		private void Aggro(Transform playerTransform)
		{
			_movementController.FollowTarget(playerTransform);
		}
	}
}