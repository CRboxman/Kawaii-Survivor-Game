using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Slider healthSlider;
    [Header("Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private TMP_Text healthText;
    [SerializeField]private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        UpdateUi();
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
        Debug.Log("deeeeeeeeeeeaaaaaaaaaaaaaddddddddddd!!!!!!");
        SceneManager.LoadScene(0);
    }
    private void UpdateUi()
    {
        float healthPercentage = health / maxHealth;
        healthSlider.value = healthPercentage;
        healthText.text = $"{health} / {maxHealth}";
    }
}
