using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CheckBoundaries : MonoBehaviour
{

    private Vector2 boundaries;

    private Transform self;
    private SpriteRenderer selfSpriteRenderer;

    private void Start()
    {

        self = transform;
        selfSpriteRenderer = GetComponent<SpriteRenderer>();

        if (selfSpriteRenderer == null || Camera.main == null)
            return;

        Vector2 objectBoundaries = new Vector2(selfSpriteRenderer.bounds.size.x / 2, selfSpriteRenderer.bounds.size.y / 2);
        Vector2 screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        boundaries = new Vector2(screenBoundaries.x - objectBoundaries.x, screenBoundaries.y - objectBoundaries.y);

    }

    private void LateUpdate()
    {

        if (selfSpriteRenderer == null || Camera.main == null)
            return;

        Vector2 position = self.position;

        position.x = Mathf.Clamp(position.x, -boundaries.x, boundaries.x);
        position.y = Mathf.Clamp(position.y, -boundaries.y, boundaries.y);

        self.position = position;

    }

}
