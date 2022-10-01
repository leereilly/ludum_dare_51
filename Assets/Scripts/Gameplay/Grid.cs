using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Helzinko
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private List<float> xPositions;

        public UnityEvent OnCubeDestroy;

        public float GetClosestX(float value)
        {
            return xPositions.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
        }
    }
}
