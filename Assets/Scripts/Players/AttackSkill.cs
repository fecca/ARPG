using UnityEngine;

namespace ARPG.Players
{
    [CreateAssetMenu(menuName = "AttackSkill")]
    public class AttackSkill : ScriptableObject
    {
        public AttackType attackType;
        public GameObject projectile;
        public float projectileForce;
    }
}