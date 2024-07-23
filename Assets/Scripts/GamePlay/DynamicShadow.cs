using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShadow : Shadow
{
    [SerializeField] float maxOffset;
    [SerializeField] bool isDynamic;

    private void LateUpdate()
    {
        if(isDynamic)
        {
            shadowSpriteRenderer.sprite = parentSpriteRenderer.sprite;
            shadowSpriteRenderer.flipX = parentSpriteRenderer.flipX;
            float offset = 0.1f;
            if(GameManager.instance.cameraFollow.maxOffset != 0)
            {
                offset = (GameManager.instance.cameraFollow.offset / GameManager.instance.cameraFollow.maxOffset) * (maxOffset * 100);
                offset /= 100;
            }
            // Do việc thay đổi scale nên không thể dùng localPosition
            shadowGameObject.transform.position = gameObject.transform.position + shadowOffset.With(x: shadowOffset.x-offset);
        }
    }
}
