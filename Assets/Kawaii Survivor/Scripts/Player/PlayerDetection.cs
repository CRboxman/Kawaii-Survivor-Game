using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Collider2D daveCollider;
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
        if (collider.TryGetComponent(out Candy candy))
        {
            if (!collider.IsTouching(daveCollider))
                return;
            candy.Collect(GetComponent<Player>());
        }
        if (collider.TryGetComponent(out Cash cash))
        {
            if (!collider.IsTouching(daveCollider))
                return;
            cash.Collect(GetComponent<Player>());
        }
    }
}
