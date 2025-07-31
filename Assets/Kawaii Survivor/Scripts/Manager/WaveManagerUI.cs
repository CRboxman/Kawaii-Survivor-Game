using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManagerUI : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text waveTimeText;
    public void UpdateWaveText(string waveString) => waveText.text = waveString;
    public void UpdateWaveTimeText(string waveTimeString) => waveTimeText.text = waveTimeString;
}
