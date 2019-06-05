using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>(); 
    }
    
    public void Move(Vector2 direction)
    {
        if (_rigidBody) _rigidBody.velocity = direction * Speed;
    }

}
