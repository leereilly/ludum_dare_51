using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    [DefaultExecutionOrder(-100)]
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private HUD hud;
        [SerializeField] private Lava lava;
        public Player player;

        public Grid grid;

        private const float waveTime = 10f;

        public static Gameplay instance;

        List<ILoadable> Loaded = new List<ILoadable>();

        private float timeSinceLastWave = 0f;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if(timeSinceLastWave >= waveTime)
            {
                lava.MoveUp();
                timeSinceLastWave = 0;
            }

            timeSinceLastWave += Time.deltaTime;

            hud.UpdateTimer(timeSinceLastWave);
        }

        public void LoadElement(ILoadable loadable)
        {
            loadable.OnLoad();
            Loaded.Add(loadable);
        }
        public void UnloadElement(ILoadable loadable)
        {
            loadable.OnUnload();
            Loaded.Remove(loadable);
        }
    }
}
