using System.Collections;
using UnityEngine;

/// <summary>
/// 可以被玩家收集的货币类，向玩家移动并在最后执行收集方法，留下了动画和收集逻辑的抽象方法供子类实现。
/// </summary>
public abstract class DropableCurrency : MonoBehaviour, ICollectable
{
    [Header("Objects")]
    [SerializeField] public Animator animator;
    private bool isCollected;


    private void OnEnable()
    {
        isCollected = false;
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
        animator.Play(GetCollectAnimationName());
        yield return new WaitForSeconds(GetCollectAnimationDelay());

        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            playerPosition = player.GetCenter();
            float t = time / duration;
            transform.position = Vector2.Lerp(spawnPosition, playerPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = player.GetCenter();
        Collected();
    }

    // 可被子类重写的动画名称
    protected virtual string GetCollectAnimationName()
    {
        return "Collect_Anim";
    }

    // 可被子类重写的动画等待时间
    protected virtual float GetCollectAnimationDelay()
    {
        return 0.3f;
    }

    // 抽象方法：由子类决定如何处理收集后逻辑
    protected abstract void Collected();
}
