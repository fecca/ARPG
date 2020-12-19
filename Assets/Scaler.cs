using UnityEngine;

public class Scaler : MonoBehaviour
{
	[SerializeField] private Vector3 from;
	[SerializeField] private Vector3 to;
	[SerializeField] private float scaleTime;

	private float _scaleTimer;
	private bool _isScaling = true;

	private void Awake()
	{
		transform.localScale = Vector3.zero;
	}

	private void Update()
	{
		if (!_isScaling) return;

		transform.localScale = Vector3.Lerp(from, to, _scaleTimer / scaleTime);
		_scaleTimer += Time.deltaTime;

		if (_scaleTimer >= scaleTime)
		{
			_isScaling = false;
			transform.localScale = to;
		}
	}
}