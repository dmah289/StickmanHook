using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAnchor : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite spriteSticked;
    [SerializeField] Sprite spriteUnsticked;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject dashLine;

    [Header("Anim Statistics")]
    [SerializeField] float animTime;
    [SerializeField] AnimationCurve animCurve;

    private bool sticked;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        sticked = false;
        Unselectable();
    }

    public void SetSticked()
    {
        spriteRenderer.sprite = spriteSticked;
        sticked = true;
        Unselectable();         // Khi bắt đầu đu -> Không cho chọn nữa
    }

    public void SetUnsticked()
    {
        spriteRenderer.sprite = spriteUnsticked;
        sticked = false;
    }

    public void Selectable()
    {
        // Khi !sticked mới được phát Anim vì có thể bestPos là điểm đang đu
        if (!sticked)
            StartCoroutine(SelectingJoint());
    }

    public void Unselectable()
    {
        StopCoroutine(SelectingJoint());
        dashLine.transform.localScale = Vector3.zero;
    }

    private IEnumerator SelectingJoint()
    {
        float time = 0f;
        Vector3 initScale = Vector3.zero;
        Vector3 endScale = Vector3.one * 1.1f;
        while(time <= animTime)
        {
            time += Time.deltaTime;
            dashLine.transform.localScale = Vector3.Lerp(initScale, endScale, animCurve.Evaluate(time));
            yield return null;
        }
    }
}
