using UnityEngine;

namespace ARPG.Characters
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SphereCollider _collider;

        private int _faintParameterId;

        private void Awake()
        {
            _faintParameterId = Animator.StringToHash("Death");
        }

        public void TakeDamage()
        {
            _collider.enabled = false;
            _animator.SetTrigger(_faintParameterId);
        }
    }
}