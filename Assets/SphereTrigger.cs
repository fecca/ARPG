using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
// [ExecuteInEditMode]
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