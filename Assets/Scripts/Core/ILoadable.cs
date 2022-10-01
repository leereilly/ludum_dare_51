using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public interface ILoadable
    {
        void OnLoad();
        void OnUnload();
    }

    public static class LoadableUtilities
    {
        public static void Load(this ILoadable loadable)
        {
            Gameplay.instance.LoadElement(loadable);
        }
        public static void Unload(this ILoadable loadable)
        {
            Gameplay.instance.UnloadElement(loadable);
        }
    }
}
