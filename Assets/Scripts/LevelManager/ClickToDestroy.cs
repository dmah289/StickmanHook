#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToDestroy : MonoBehaviour
{
    private void OnMouseDown()
    {
        if(gameObject != null && CreateLevelData.instance.currentObject == TypeObject.Remove)
        {
            gameObject.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
#endif