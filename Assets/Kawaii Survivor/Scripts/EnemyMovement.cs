using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }
        else
        {
            Debug.Log("Player GameObject found: " + player.name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir =(player.transform.position - transform.position).normalized ;
        transform.position =Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
