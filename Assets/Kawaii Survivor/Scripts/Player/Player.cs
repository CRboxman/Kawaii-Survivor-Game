using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth),typeof(PlayerLevel))]
public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Objects")]
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;
    private PlayerController playerController;
    [SerializeField ]private CircleCollider2D playerCollider;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerHealth = GetComponent<PlayerHealth>();
        playerLevel = GetComponent<PlayerLevel>();
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToTakeDamage(float Damage)
    {
        playerHealth.TakeDamage(Damage);
    }
    public Vector2 GetCenter()
    {
        return (Vector2)transform.position + playerCollider.offset;
    }
    public bool HasLevelUp()
    {
        return playerLevel.HasLevelUp();
    }
    public void CanMove(bool canMove)
    {
        playerController.canMove = canMove;
    }
}
