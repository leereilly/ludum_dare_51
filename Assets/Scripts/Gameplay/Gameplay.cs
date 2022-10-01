using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Gameplay : MonoBehaviour
    {
        public static Gameplay instance;

        List<ILoadable> Loaded = new List<ILoadable>();

        private void Awake()
        {
            instance = this;
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
