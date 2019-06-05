using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerLifeManager : LifeManager
{

    [Header("Shield Data")]

    public Transform ShieldPrefab;
    public Vector3 ShieldOffset = new Vector3(0, 0.25f, 0);

    public float ShieldTime = 10;
    public float ShieldEndWarningTime = 3;

    private bool isShieldUp = false;
    private bool isShieldDeactivating = false;

    private Transform shield;

    private readonly float coroutineTimeStep = 0.15f;

    private float timer;

    private void Update()
    {

        if (isShieldUp)
        {

            timer += Time.deltaTime;

            if (timer >= ShieldTime - ShieldEndWarningTime && isShieldDeactivating == false)
            {
                isShieldDeactivating = true;
                StartCoroutine(DeactivateShield());
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isShieldUp == false && (other.CompareTag("Enemy") || other.CompareTag("Obstacle")))
            Hit();
    }

    private IEnumerator DeactivateShield()
    {

        SpriteRenderer spriteRenderer = shield.GetComponent<SpriteRenderer>();

        for (int counter = 0; counter < (int)(ShieldEndWarningTime / coroutineTimeStep); counter++)
        {

            if (spriteRenderer)
            {
                Color color = spriteRenderer.color;
                color.a = (color.a == 1) ? 0.55f : 1.0f;
                spriteRenderer.color = color;
            }

            yield return new WaitForSeconds(coroutineTimeStep);

        }

        isShieldUp = false;
        isShieldDeactivating = false;

        Destroy(shield.gameObject);

        yield return null;

    }

    public void ActiveShield()
    {

        if (ShieldPrefab == null)
            return;

        if (isShieldUp)
        {
            timer = 0;
            return;
        }

        isShieldUp = true;
        timer = 0;

        shield = Instantiate(ShieldPrefab, self.position + ShieldOffset, Quaternion.identity, self);

    }

    public override void Hit()
    {
        if (isShieldUp == false)
            base.Hit();
    }

}
