using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour,ICollectable
{
    [Header("Objects")]
    [SerializeField] public Animator candyAnimator;
    private bool isCollected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Collect(Player player)
    {
        if (isCollected)
            return;
        isCollected = true;

        StartCoroutine(MoveToPlayer(player));
    }
    IEnumerator MoveToPlayer(Player player)
    {
        Vector2 spawnPosition = transform.position;
        Vector2 playerPosition = player.GetCenter();
        // 播放动画
        candyAnimator.Play("Collect_Anim");
        yield return new WaitForSeconds(0.3f); // 等待动画
        float duration = 1f;
        float time = 0f;
        while (time < duration)
        {
            playerPosition = player.GetCenter(); // 如果玩家会移动，就每帧更新目标位置
            float t = time / duration; // 归一化时间 [0,1]
            transform.position = Vector2.Lerp(spawnPosition, playerPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        // 最后强制对齐一次，避免插值小误差
        transform.position = player.GetCenter();
        Collected();
    }

    private void Collected()
    {
        gameObject.SetActive(false);
    }
}
