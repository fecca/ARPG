using System;
using UnityEngine;
using Zenject;

namespace ARPG.Players
{
	public class Projectile : MonoBehaviour
	{
		private Vector3 _position;
		private Vector3 _direction;
		private float _lifeTime;

		[Inject]
		public void Construct(Vector3 position, Vector3 direction, float lifeTime)
		{
			_position = position;
			_direction = direction;
			_lifeTime = lifeTime;
		}

		private void Start()
		{
			transform.position = _position;
			GetComponent<Rigidbody>().AddForce(_direction * 1000f);
			Destroy(gameObject, _lifeTime);
		}

		public class Factory : PlaceholderFactory<Vector3, Vector3, float, Projectile>
		{
		}
	}
}