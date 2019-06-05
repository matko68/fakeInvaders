using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTypeLaser : Pickup
{
    private void Start()
    {
        Type = PickupType.LASER;
        Move();
    }
}
