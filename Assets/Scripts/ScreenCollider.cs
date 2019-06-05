using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ScreenCollider : MonoBehaviour
{

    private void Awake()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider && Camera.main)
            collider.size = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) * 2;
    }

}
