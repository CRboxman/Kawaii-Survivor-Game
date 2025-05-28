using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Animator DamageTextAnimator;
    [SerializeField]private TMP_Text damageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimate(float damage)
    {
        damageText.text = damage.ToString();
        DamageTextAnimator.Play("Damage_Text");
    }
}
