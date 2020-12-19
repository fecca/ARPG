using System;
using UnityEngine;

public class Rotater : MonoBehaviour
{
	[SerializeField] private float speed;

	private void Update()
	{
		transform.Rotate(0, Time.deltaTime * speed, 0, Space.World);
	}
}