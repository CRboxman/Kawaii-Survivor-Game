using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]private Slider levelSlider;
    [SerializeField] private TMP_Text levelText;
    [Header("Settings")]
    [SerializeField] private int requireXp;
    [SerializeField] private int currentXp;
    [SerializeField] private int level;
    [SerializeField] private int levelsEarnThisWave;
    // Start is called before the first frame update
    void Start()
    {
        Candy.onCollected += AddXp; // 订阅糖果收集事件
        levelText.text = $"Level\t{level}\t ({currentXp}/{requireXp})";
        levelSlider.value = (float)currentXp / requireXp;
    }

    // Update is called once per frame
    void Destroy()
    {
        Candy.onCollected -= AddXp; // 取消订阅糖果收集事件
    }
    private void updateRequireXP()
    {
        requireXp+= level*5;
    }
    private void updateSlider()
    {
        levelSlider.value = (float)currentXp / requireXp;
        levelText.text = $"Level\t{level}\t ({currentXp}/{requireXp})";
    }
    public void AddXp(Candy candy)
    {
        currentXp += 1;
        if (currentXp >= requireXp)
        {
            LevelUp();
        }
        updateSlider();
    }

    private void LevelUp()
    {
        level++;
        levelsEarnThisWave++;
        currentXp =0;
        updateRequireXP();
    }

   public bool HasLevelUp()
    {
        //Debug.Log($"Levels Earn This Wave: {levelsEarnThisWave}");
        if (levelsEarnThisWave > 0)
        {
            levelsEarnThisWave--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
