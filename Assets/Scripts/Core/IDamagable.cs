using UnityEngine;

namespace Helzinko
{
    public interface IDamagable
    {
        public enum DamageType { Player, Enemy, Lava, Cube }

        void TakeDamage(float amount, DamageType type, Vector2 point);
    }
}
