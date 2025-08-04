using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IPlayerStatesDependency
{
    [Header("Objects")]
    [SerializeField] private Slider healthSlider;
    [Header("Settings")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private float baseMaxHealth;
    [SerializeField] private float maxHealth;
    [SerializeField]private float health;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float Damage)
    {
        float realDamage = Mathf.Clamp(Damage, 0f, health);
        health -= Damage;
        UpdateUi();
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
    }
    private void Die()
    {
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }
    private void UpdateUi()
    {
        float healthPercentage = health / maxHealth;
        healthSlider.value = healthPercentage;
        healthText.text = $"{health} / {maxHealth}";
    }

    public void UpdateStats(PlayerStateManager playerStateManager)
    {
        float addedHealth = playerStateManager.GetPlayerStateValue(PlayerState.MaxHealth);
        maxHealth = baseMaxHealth + addedHealth;
        maxHealth=Mathf.Max(maxHealth, 1f); // 确保最大生命值至少为1

        health = maxHealth;
        UpdateUi();
    }
}
