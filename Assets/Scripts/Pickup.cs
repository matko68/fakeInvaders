using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType Type;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void Move()
    {
        if (_rigidbody2D)
            _rigidbody2D.velocity = 3.5f * Vector2.down;
    }

}
