using System.Linq;
using ARPG.Characters;
using UnityEngine;

namespace ARPG.Debugging
{
	public class DebugInputHandler : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.K))
			{
				FindObjectsOfType<Enemy>().ToList().ForEach(ec => ec.Kill());
			}
		}
	}
}