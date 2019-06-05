using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{

    private Transform _transform;
    private ShootingManager _shootingManager;
    private PlayerLifeManager _lifeManager;

    private void Awake()
    {
        _transform = transform;
        _shootingManager = GetComponent<ShootingManager>();
        _lifeManager = GetComponent<PlayerLifeManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pick-up"))
        {
            Pickup pickUp = other.GetComponent<Pickup>();
            if (pickUp.Type == PickupType.LASER && _shootingManager)
                _shootingManager.BoostLaser();

            if (pickUp.Type == PickupType.SHIELD && _lifeManager)
                _lifeManager.ActiveShield();
            Destroy(other.gameObject);
        }
    }
}
