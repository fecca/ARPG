using UnityEngine;

namespace ARPG.Characters
{
	[CreateAssetMenu(menuName = "CameraConfig")]
	public class CameraConfig : ScriptableObject
	{
		public Vector3 localPosition;
		public Vector3 localRotation;
	}
}