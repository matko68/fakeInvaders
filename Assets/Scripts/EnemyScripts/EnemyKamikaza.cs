using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaza : MonoBehaviour
{

    private PlayerController player;

    private float time = 0.25f;
    private float timer;
    private Rigidbody2D rigi;
    private float fac = 5.5f;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        timer = 0;
        rigi = GetComponent<Rigidbody2D>();
        //rigi.AddForce(Vector2.down * 20);
    }

    private void Update()
    {

        timer -= Time.deltaTime;
        //Debug.Log(Time.deltaTime);

        if (timer <= 0.0f)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            //rigi.velocity = Vector2.zero;
            rigi.velocity *= 0.25f;
            rigi.AddForce(dir * fac);
            fac += 2.5f;
            timer = time;
        }

    }

}
