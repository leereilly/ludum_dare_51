using UnityEngine;

namespace Helzinko
{
    public class GameEntity : MonoBehaviour, ILoadable, IDamagable
    {
        public float health;

        public virtual void OnLoad()
        {

        }

        public virtual void OnUnload()
        {
            Destroy(gameObject);
        }

        public virtual void TakeDamage(float amount, IDamagable.DamageType type, Vector2 point)
        {
            health -= amount;

            if (health <= 0) Kill(type);
        }

        public virtual void Kill(IDamagable.DamageType type)
        {
            this.Unload();
        }
    }
}
