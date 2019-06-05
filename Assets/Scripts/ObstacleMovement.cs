using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    public Vector2 SpeedInterval;
    public Vector2 Direction;

    private void Awake()
    {

        transform.eulerAngles = new Vector3(0, 0, new Vector2(0, 360).RandomValue());

        Rigidbody2D selfRigidbody = GetComponent<Rigidbody2D>();

        if (selfRigidbody)
            selfRigidbody.velocity = SpeedInterval.RandomValue() * Direction;

    }

}
