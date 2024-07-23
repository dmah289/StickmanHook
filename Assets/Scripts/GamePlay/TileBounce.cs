using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBounce : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] bool isAnimating;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAnimating && collision.gameObject.tag.Equals(KeySave.stickman))
        {
            anim.SetTrigger(KeySave.touched);
            isAnimating = true;
        }
    }

    private void Update()
    {
        if (isAnimating && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            isAnimating = false;
        }
    }
}
