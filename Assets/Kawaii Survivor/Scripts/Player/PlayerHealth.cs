using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IPlayerStatesDependency, IGameStateListener
{
    [Header("Objects")]
    [SerializeField] private Slider healthSlider;
    private bool gameStarted = false;
    [Header("Settings（baseMaxHealth，healthToAddValue需要自己手动设置！！）")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField][Tooltip("基础生命值")] private float baseMaxHealth;
    [SerializeField][Tooltip("目前加上增益的最大生命值")] private float maxHealth;
    [SerializeField][Tooltip("当前实际生命值")] private float health;
    [SerializeField][Range(0, 0.25f)] private float healthToAddValue;
    [SerializeField][Range(0, 80)] private float armor;
    [SerializeField][Range(0, 100)] private float lifeSteal;
    [SerializeField][Range(0, 80)] private float dodgeChance;
    [SerializeField][Range(0, 20)] private float healthRecoverSpeed;
    [HorizontalLine]
    [SerializeField][Range(0, 80)] private float healthRecoverDuration;
    [SerializeField] private float healthRecoverTimer;
    public static Action<Vector2> OnAttackDodge;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyTakeDamageCallBack;
    }
    // Start is called before the first frame update
    void Start()
    {
        health  = maxHealth;
        UpdateUi();
    }
    // Update is called once per frame
    void Update()
    {
        if (health < maxHealth)
        {
            RecoverHealth();
        }
    }
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyTakeDamageCallBack;
    }
    private void RecoverHealth()
    {
        if (!gameStarted)
            return;
        healthRecoverTimer += Time.deltaTime;
        if (healthRecoverTimer >= healthRecoverDuration)
        {
            healthRecoverTimer = 0f;
            float healthToAdd = MathF.Min(healthToAddValue, maxHealth - health);
            health += healthToAdd;
            UpdateUi();
        }
    }
    private void EnemyTakeDamageCallBack(float damage, Vector2 pos, bool isCriticalHit)
    {
        if (health >= maxHealth)
            return;
        float healValue = damage * (lifeSteal / 100f);
        float effectiveHeal = MathF.Min(healValue, maxHealth - health);
        health += effectiveHeal;
        UpdateUi();
    }

    public void TakeDamage(float damage)
    {
        if (ShouldDodge())
        {
            OnAttackDodge?.Invoke(transform.position);
            return;
        }
        armor = Mathf.Clamp(armor, 0f, 80f);
        float realDamage = (100 - armor) / 100 * Mathf.Clamp(damage, 0, 300);
        realDamage = MathF.Min(realDamage, health);
        health -= realDamage;
        UpdateUi();
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
    }

    private bool ShouldDodge()
    {
        dodgeChance = Mathf.Clamp(dodgeChance, 0f, 80f);
        return UnityEngine.Random.Range(0f, 100f) < dodgeChance;
    }

    private void Die()
    {
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }
    private void UpdateUi()
    {
        float healthPercentage = health / maxHealth;
        healthSlider.value = healthPercentage;
        healthText.text = health.ToString("F2") + "/" + maxHealth.ToString("F2");
    }

    public void UpdateStats(PlayerStateManager playerStateManager)
    {
        float addedHealth = playerStateManager.GetPlayerStateValue(PlayerState.MaxHealth);
        maxHealth = baseMaxHealth + addedHealth;
        maxHealth = Mathf.Max(maxHealth, 1f); // 确保最大生命值至少为1

        health = maxHealth;
        UpdateUi();
        armor = playerStateManager.GetPlayerStateValue(PlayerState.Armor);
        lifeSteal = playerStateManager.GetPlayerStateValue(PlayerState.LifeSteal);
        dodgeChance = playerStateManager.GetPlayerStateValue(PlayerState.Dodge);

        healthRecoverSpeed = Mathf.Clamp(playerStateManager.GetPlayerStateValue(PlayerState.HealthRecoverySpeed), 0, 15);
        healthRecoverDuration = 1 / healthRecoverSpeed;
    }
    public void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                gameStarted = false;
                break;
            case GameState.GAME:
                gameStarted = true;
                break;
            case GameState.WAVETRANSITION:
                gameStarted = false;
                break;
            case GameState.SHOP:
                gameStarted = false;
                break;
            case GameState.GAMEOVER:
                gameStarted = false;
                break;
            case GameState.WEAPON_SELECT:
                gameStarted = false;
                break;
        }
    }
}
