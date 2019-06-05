using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObstacleLifeManager : LifeManager
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Screen"))
            isActive = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Screen"))
            Destroy(gameObject);
    }

}
