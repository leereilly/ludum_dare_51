using UnityEngine;

namespace Helzinko
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        Player player;

        private const float flipAngle = 90f;

        public void Init(Player player)
        {
            this.player = player;
        }

        void Update()
        {
            Vector3 difference = player.input.aimVector;
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
            spriteRenderer.transform.localScale = new Vector3(1, (rotation_z > flipAngle || rotation_z < -flipAngle ? -1 : 1), 1);
        }
    }
}
