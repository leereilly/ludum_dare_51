using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Helzinko
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timer;

        public void UpdateTimer(float value)
        {
            timer.SetText(FormattedTime(value));
        }

        public static string FormattedTime(float time)
        {
            int millisTotal = Mathf.RoundToInt(time * 1000);

            int minutes = millisTotal / 60000;
            millisTotal -= minutes * 60000;
            int seconds = (millisTotal) / 1000;
            millisTotal -= seconds * 1000;
            int millis = (millisTotal - (millisTotal % 10)) / 10;
            return $"{seconds:D2}:{millis:D2}";
        }
    }
}
