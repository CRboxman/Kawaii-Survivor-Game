using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("Objects")]
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
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
}
