using UnityEngine;

namespace ARPG.Attacking
{
    [CreateAssetMenu(menuName = "AttackSkill")]
    public class AttackSkill : ScriptableObject
    {
        public AttackType attackType;
        public float range;
        public float projectileSpeed;
    }
}