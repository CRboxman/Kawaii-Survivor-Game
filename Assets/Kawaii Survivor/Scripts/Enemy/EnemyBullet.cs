using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]private Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField]private float damage;
    [SerializeField]private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(float damage,Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        rb.velocity = direction * bulletSpeed; // Adjust speed as necessary
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.ToTakeDamage(1f);
            Destroy(gameObject);
        }
    }
}
