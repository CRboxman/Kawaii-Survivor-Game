using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Animator DamageTextAnimator;
    [SerializeField] private TMP_Text damageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayAnimate(float damage, bool isCriticalHit)
    {
        damageText.text = damage.ToString();
        if (isCriticalHit==true)
        {
            damageText.color = Color.red; // 红色不透明
        }
        else
        {
            damageText.color = Color.yellow; // 白色不透明
        }

        DamageTextAnimator.Play("Damage_Text");
    }
}
