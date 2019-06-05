using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{

    private int _index = 0;
    private int _step = 0;
    private int _maxStep;

    private int _pointsCount = 10000;
    private List<Vector2> _points = new List<Vector2>();

    private int _quadrant;
    private Dictionary<int, List<Vector2>> _quadrants = new Dictionary<int, List<Vector2>>
    {
        {1, new List<Vector2> { new Vector2(0.0f, 7.5f), new Vector2(1.5f, 4.0f)}},
        {2, new List<Vector2> { new Vector2(-7.5f, 0.0f), new Vector2(1.5f, 4.0f)}},
        {3, new List<Vector2> { new Vector2(-7.5f, 0.0f), new Vector2(-1.0f, 1.5f)}},
        {4, new List<Vector2> { new Vector2(0.0f, 7.5f), new Vector2(-1.0f, 1.5f)}}
    };

    private float _timer;

    private Transform _transform;

    //private List<Vector2> controlPoints = new List<Vector2>();

    private float t;
    private float speed = 6;
    private float speedFactor;
    private float targetStepSize;
    private float lastStepSize;

    private void Awake()
    {

        #region FINDING THE INITIAL QUADRANT

        _quadrant = 0;

        foreach (int quadrant in _quadrants.Keys)
        {
            if (_quadrants[quadrant][0].WithinRange(transform.position.x) && _quadrants[quadrant][1].WithinRange(transform.position.y))
            {
                _quadrant = quadrant;
                break;
            }
        }

        #endregion

        _transform = transform;

        CalculateBezierCurvePath();

        speedFactor = speed / 10;
        targetStepSize = speed / 60;
    }

    private void CalculateBezierCurvePath()
    {

        List<Vector2> controlPoints = new List<Vector2> { _transform.position };
        List<int> quadrants = new List<int> { _quadrant };

        for (int counter = 1; counter < 4; counter++)
        {
            quadrants.Add((_quadrant - 1 + counter) % 4 + 1);
            controlPoints.Add(new Vector2(_quadrants[quadrants[quadrants.Count - 1]][0].RandomValue(), _quadrants[quadrants.Count - 1][1].RandomValue()));
        }

        _quadrant = quadrants[3];

        List<Vector2> points = new List<Vector2>();

        for (int counter = 0; counter < _pointsCount - 1; counter++)
            points.Add(CalculateBezierCurvePoint(((float)counter) / _pointsCount, controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3]));

        points.Add(controlPoints[3]);

        _points.Clear();
        _points.Add(points[0]);

        for (int index = 1; index < _pointsCount; index++)
            _points.Add(_points[index - 1] + (points[index] - points[index - 1]).normalized * 0.15f);

    }

    private Vector2 CalculateBezierCurvePoint(float t, Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3)
    {

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float ttt = tt * t;
        float uuu = uu * u;

        Vector2 point = uuu * P0;
        point += 3 * uu * t * P1;
        point += 3 * u * tt * P2;
        point += ttt * P3;

        return point;

    }

    private void Update()
    {
        
        if (_index < _pointsCount)
        {
            _transform.position = _points[_index];
            _index++;
            Debug.Log((_points[_index] - _points[_index - 1]).magnitude);
        }

        else
        {
            CalculateBezierCurvePath();
            _index = 0;

        }
        /*
        if (t == 0)
        {
            controlPoints.Clear();
            controlPoints.Add(_transform.position);
            List<int> quadrants = new List<int> { _quadrant };

            for (int counter = 1; counter < 4; counter++)
            {
                quadrants.Add((_quadrant - 1 + counter) % 4 + 1);
                controlPoints.Add(new Vector2(_quadrants[quadrants[quadrants.Count - 1]][0].RandomValue(), _quadrants[quadrants.Count - 1][1].RandomValue()));
            }
            //t = 0;
        }

        if (true)
        {

            Vector3 prevPosition = _transform.position;
            _transform.position = CalculateBezierCurvePoint(t, controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3]);
            lastStepSize = Vector3.Magnitude(transform.position - prevPosition);
            if (lastStepSize < targetStepSize)
                speedFactor *= 1.1f;
            else
                speedFactor *= 0.9f;
            t += speedFactor * Time.deltaTime;
            _index++;
        }*/

        /*if (t >= 1)
            t = 0;*/

    }

}
