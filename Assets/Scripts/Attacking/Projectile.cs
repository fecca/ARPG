using ARPG.Characters;
using UnityEngine;
using Zenject;

namespace ARPG.Attacking
{
	public class Projectile : MonoBehaviour
	{
		private Vector3 _startPosition;
		private Vector3 _direction;
		private float _range;

		[Inject]
		public void Construct(Vector3 position, Vector3 direction, float range)
		{
			_startPosition = position;
			_direction = direction;
			_range = range;
		}

		private void Start()
		{
			transform.position = _startPosition;
		}

		private void FixedUpdate()
		{
			transform.Translate(_direction);

			if ((transform.position - _startPosition).sqrMagnitude > _range * _range)
			{
				Destroy(gameObject);
			}
		}

		private void OnCollisionEnter(Collision other)
		{
			var enemy = other.collider.GetComponent<EnemyController>();
			if (enemy != null)
			{
				enemy.TakeDamage();
			}

			Destroy(gameObject);
		}

		public class Factory : PlaceholderFactory<Vector3, Vector3, float, Projectile>
		{
		}
	}
}