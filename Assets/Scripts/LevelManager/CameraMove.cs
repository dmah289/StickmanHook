using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float moveX;
    [SerializeField] float speed;
    
    void Update()
    {
        moveX = Input.GetAxisRaw(KeySave.horizontal);
        transform.Translate(Vector2.right * moveX * speed * Time.deltaTime);
    }
}
