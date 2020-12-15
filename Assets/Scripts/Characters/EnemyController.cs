using UnityEngine;

namespace ARPG.Characters
{
    public class EnemyController : MonoBehaviour
	{
		public void TakeDamage()
		{
			Destroy(gameObject);
		}
	}
}