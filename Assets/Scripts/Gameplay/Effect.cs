using UnityEngine;
using DG.Tweening;

namespace Helzinko
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private float time;

        public SpriteRenderer sp;

        Tween killTween;

        private void Start()
        {
            killTween = DOVirtual.DelayedCall(time, () => Destroy(gameObject), false).SetTarget(gameObject);
        }

        private void OnDisable()
        {
            killTween?.Kill();
        }

        private void OnDestroy()
        {
            killTween?.Kill();
        }

    }
}
