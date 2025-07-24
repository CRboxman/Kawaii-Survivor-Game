using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{

    private bool isCollected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Collect(Player targetPosition)
    {
        if (isCollected)
            return;
        isCollected = true;

        StartCoroutine(MoveToPlayer(targetPosition));
    }
    IEnumerator MoveToPlayer(Player targetPosition)
    {
        float time = 0;
        Vector2 spawnPosition = transform.position;
        Vector2 playerPosition = targetPosition.GetCenter();
        while (time < 1)
        {
            playerPosition = targetPosition.GetCenter();
            transform.position = Vector2.Lerp(spawnPosition, playerPosition, time);
            time += Time.deltaTime;
            yield return null;
        }
        Collected();
    }
    private void Collected()
    {
        gameObject.SetActive(false);
    }
}
