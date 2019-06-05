using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Transform _transform;
    private Rigidbody2D _rigidBody2D;
    private Vector2 _screenSize;
    private float _ymin;
    private float _xmin;
    private Transform Player;
    private float p;
    private float q;
    private float difp;
    private float difq;
    private Vector2 _timeInterval = new Vector2(0, 0.75f);
    private float timer;



    private void Awake()
    {
        _transform = transform;
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _screenSize = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        _ymin = _screenSize.y * (-1 / 2);
        _xmin = _screenSize.x * (-1 / 2);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = _timeInterval.RandomValue();
    }

    private void Update()
    {
          timer -= Time.deltaTime;
          if (timer <= 0)
          {
              timer = _timeInterval.RandomValue();
              ChangeDirection();
          }

    }

    private void ChangeDirection()
    {
        p = -1;
        q = 1;
        if (_rigidBody2D.velocity.x < 0) p -= 1;
        else if (_rigidBody2D.velocity.x > 0) q += 1;

        float difp = Mathf.Abs(_xmin - _transform.position.x);
        float difq = 1 - difp;
        p -= difp;
        q += difq;

        float x = new Vector2(p, q).RandomValue();

        p = -1;
        q = 1;
        if (_rigidBody2D.velocity.y < 0) p -= 1;
        else if (_rigidBody2D.velocity.y > 0) q += 1;

        difp = Mathf.Abs(_ymin - _transform.position.y);
        difq = 1 - difp;
        p -= difp;
        q += difq;

        float y = new Vector2(p, q).RandomValue();
        if (Player.position.y > _transform.position.y) y = Mathf.Abs(y);



        _rigidBody2D.velocity += new Vector2(x, y);
        _rigidBody2D.velocity = _rigidBody2D.velocity.normalized * new Vector2(1, 2).RandomValue();
        





    }

}
