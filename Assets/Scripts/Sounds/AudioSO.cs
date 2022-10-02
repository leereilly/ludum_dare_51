using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    [CreateAssetMenu(fileName = "AudioSO", menuName = "ScriptableObjects/AudioSO", order = 1)]
    public class AudioSO : ScriptableObject
    {
        public Sound[] sounds;
    }
}