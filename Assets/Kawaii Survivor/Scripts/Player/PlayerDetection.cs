using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] [Tooltip("去子对象上找检测碰撞，改范围")]private Collider2D collectableCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out ICollectable collectable))
        {
            if (!collider.IsTouching(collectableCollider))
                return;
            collectable.Collect(GetComponent<Player>());
        }
    }
}
