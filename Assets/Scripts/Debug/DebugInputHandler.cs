using System.Linq;
using ARPG.Characters;
using UnityEngine;

namespace ARPG.Debug
{
	public class DebugInputHandler : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.K))
			{
				FindObjectsOfType<EnemyController>().ToList().ForEach(ec => ec.Kill());
			}
		}
	}
}