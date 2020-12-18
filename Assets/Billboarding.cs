using UnityEngine;
using Zenject;

public class Billboarding : MonoBehaviour
{
	private Camera _cam;

	[Inject]
	public void Construct(Camera cam)
	{
		_cam = cam;
	}

	private void Update()
	{
		transform.forward = _cam.transform.forward;
	}
}