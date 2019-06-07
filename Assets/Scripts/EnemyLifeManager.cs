
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class EnemyLifeManager : LifeManager
{

    public static UnityEvent EnemyKilled = new UnityEvent();
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Screen"))
            IsActive = true;
    }*/

    public override void Hit()
    {
        if (lives == 1)
            EnemyKilled.Invoke();
        base.Hit();
    }

}