using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLoop : MonoBehaviour
{

    public List<Transform> ImagePrefabs = new List<Transform>();

    public float LoopSpeed;

    private List<Transform> _images = new List<Transform>();

    private float _loopSpeed;

    private Vector2 _imageSize;
    private Vector2 _screenSize;

    private Transform _transform;

    private void Awake()
    {

        if (ImagePrefabs.Count == 0)
            return;

        SpriteRenderer imageSpriteRenderer = ImagePrefabs[0].GetComponent<SpriteRenderer>();

        if (imageSpriteRenderer == null || Camera.main == null)
            return;

        _transform = transform;

        _imageSize = imageSpriteRenderer.bounds.max - imageSpriteRenderer.bounds.min;
        _screenSize = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        for (int counter = 0; counter < 2; counter++)
            _images.Add(Instantiate(ImagePrefabs[Random.Range(0, ImagePrefabs.Count)], new Vector2(0, counter * _imageSize.y + (_imageSize.y - _screenSize.y) / 2), Quaternion.identity, _transform));

        _loopSpeed = LoopSpeed;

    }

    private void Update()
    {
        if (_images.Count == 0)
            return;

        if (_images[0].position.y <= (_imageSize.y - _screenSize.y) / 2 - _imageSize.y)
        {

            Transform image = _images[0];
            _images.RemoveAt(0);
            Destroy(image.gameObject);

            _images.Add(Instantiate(ImagePrefabs[Random.Range(0, ImagePrefabs.Count)], new Vector2(0, _images[_images.Count - 1].position.y + _imageSize.y), Quaternion.identity, _transform));
            
        }
    }

    private void LateUpdate()
    {
        for (int index = _images.Count - 1; index >= 0; index--)
            _images[index].position += new Vector3(0, _loopSpeed, 0) * Time.deltaTime;

    }

    private void ChangeLoopSpeed(float speed)
    {
        _loopSpeed = speed;
    }
}


