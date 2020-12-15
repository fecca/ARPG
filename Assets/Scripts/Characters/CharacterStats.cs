using UnityEngine;

namespace ARPG.Characters
{
    [CreateAssetMenu(menuName = "CharacterStats")]
    public class CharacterStats : ScriptableObject
    {
        public int health;
        public float movementSpeed;
        public float attackSpeed;
    }
}