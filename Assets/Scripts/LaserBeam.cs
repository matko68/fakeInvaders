using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LaserBeam : MonoBehaviour
{

    public List<string> ObjectsToHitTags = new List<string>();

    private Transform self;
    private AudioSource selfAudioSource;
    private Rigidbody2D selfRigidbody;

    private void Awake()
    {
        self = transform;
        selfAudioSource = GetComponent<AudioSource>();
        selfRigidbody = GetComponent<Rigidbody2D>();
        Shoot(Vector2.up, 5.5f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Screen"))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ObjectsToHitTags.Contains(other.tag))
        {
            //LifeManager manager = other.GetComponent<LifeManager>();
            //if (manager)
              //  manager.Hit();
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector3 direction, float speed)
    {

        self.eulerAngles += new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);

        if (selfAudioSource)
            selfAudioSource.Play();

        if (selfRigidbody)
            selfRigidbody.velocity = direction * speed;

    }

}
