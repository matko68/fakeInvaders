using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTypeShield : Pickup
{
    private void Start()
    {
        Type = PickupType.SHIELD;
        Move();
    }
}
