using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Helzinko
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI metersText;

        [SerializeField] private TextMeshProUGUI timerResultText;
        [SerializeField] private TextMeshProUGUI metersResultText;

        [SerializeField] private GameObject[] barTiles;

        public void UpdateVoidBar(int level)
        {
            for (int i = 0; i < barTiles.Length; i++)
            {
                barTiles[i].SetActive(i < level);
            }
        }

        public void UpdateResults(int meters, float time)
        {
            timerResultText.SetText("Time survived: <color=#ff5dcc>" + FormattedTime(time) + "</color>");
            metersResultText.SetText("Meters Reched: <color=#ff5dcc>" + meters.ToString() + "</color> m");
        }

        public void UpdateTimer(float time)
        {
            timerText.SetText(FormattedTime(time));
        }

        public void UpdateMeters(int meters)
        {
            metersText.SetText(meters.ToString() + " m");
        }

        public void ActiveStartText(bool active)
        {
            startText.gameObject.SetActive(active);
        }

        public void ActiveResultText(bool active)
        {
            timerResultText.gameObject.SetActive(active);
            metersResultText.gameObject.SetActive(active);
        }

        public static string FormattedTime(float time)
        {
            int millisTotal = Mathf.RoundToInt(time * 1000);

            int minutes = millisTotal / 60000;
            millisTotal -= minutes * 60000;
            int seconds = (millisTotal) / 1000;
            millisTotal -= seconds * 1000;
            int millis = (millisTotal - (millisTotal % 10)) / 10;
            return $"{minutes:D2}:{seconds:D2}:{millis:D2}";
        }
    }
}
