using System;
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
		private float _speed;

		[Inject]
		public void Construct(Vector3 position, Vector3 direction, float speed, float range)
		{
			_startPosition = position;
			_direction = direction;
			_speed = speed;
			_range = range;
		}

		private void Start()
		{
			transform.position = _startPosition;
		}

		private void FixedUpdate()
		{
			transform.Translate(_direction * Time.deltaTime * _speed);

			if ((transform.position - _startPosition).sqrMagnitude > _range * _range)
			{
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			Destroy(gameObject);
		}

		public class Factory : PlaceholderFactory<Vector3, Vector3, float, float, Projectile>
		{
		}
	}
}