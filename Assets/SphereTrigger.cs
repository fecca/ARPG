using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SphereTrigger : MonoBehaviour
{
	private Action<Collider> _onTrigger = _ => { };
	private SphereCollider _sphereCollider;
	private Rigidbody _rigidbody;

	private void Awake()
	{
		_sphereCollider = GetComponent<SphereCollider>();
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		_sphereCollider.enabled = true;
	}

	private void OnDisable()
	{
		_sphereCollider.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		_onTrigger(other);
	}

	public void Initialize(float aggroRadius, Action<Collider> onTrigger)
	{
		_onTrigger = onTrigger;
		_sphereCollider.radius = aggroRadius;
		_sphereCollider.isTrigger = true;
		_rigidbody.useGravity = false;
		_rigidbody.isKinematic = true;
		_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
}