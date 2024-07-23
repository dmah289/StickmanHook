using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] Material shadowMaterial;

    [SerializeField] protected Vector3 shadowOffset;
    [SerializeField] protected SpriteRenderer parentSpriteRenderer;

    protected SpriteRenderer shadowSpriteRenderer;
    protected GameObject shadowGameObject;
    
    private void Awake()
    {
        parentSpriteRenderer = GetComponent<SpriteRenderer>();
        shadowGameObject = new GameObject(KeySave.shadow2d);

        shadowSpriteRenderer = shadowGameObject.AddComponent<SpriteRenderer>();
        shadowSpriteRenderer.sprite = parentSpriteRenderer.sprite;
        shadowSpriteRenderer.material = shadowMaterial;
        shadowSpriteRenderer.sortingLayerName = parentSpriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder - 1;

        shadowGameObject.transform.parent = gameObject.transform;
        shadowGameObject.transform.localScale = Vector3.one;
        shadowGameObject.transform.rotation = gameObject.transform.rotation;
        shadowGameObject.transform.localPosition = (Vector3)shadowOffset;
    }
}
