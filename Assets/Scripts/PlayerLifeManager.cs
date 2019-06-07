using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerLifeManager : LifeManager
{

    private PlayerActionManager actionManager;

    private void Start()
    {
        actionManager = GetComponent<PlayerActionManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (actionManager.IsShieldUp == false && (other.CompareTag("Enemy") || other.CompareTag("Obstacle")))
        {
            other.GetComponent<LifeManager>().Hit();
            Hit();
        }

    }


    public override void Hit()
    {
        if (actionManager.IsShieldUp == false)
            base.Hit();
    }

}
