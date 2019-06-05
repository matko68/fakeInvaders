using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBezier : EnemyMovement
{
    public float duration = 2;
    public float way;
    public float dist;
    public float f;


    private int _index = 0;
    //private int _step = 0;
    //private int _maxStep;

    private int _pointsCount = 100;
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
    }
    
    private void CalculateBezierCurvePath()
    {

        List<Vector2> controlPoints = new List<Vector2> { _transform.position};
        List<int> quadrants = new List<int> { _quadrant};

        quadrants.Add(_quadrant + 1);
        quadrants.Add(_quadrant + 1);
        quadrants.Add(_quadrant + 2);

        for (int i = 0; i < 4; i++)
        {
            if (quadrants[i] > 4)
                quadrants[i] -= 4;
        }

        for (int counter = 1; counter < 4; counter++)
        {
            //quadrants.Add((_quadrant - 1 + counter) % 4 + 1);
            Debug.Log(quadrants[counter]);
            controlPoints.Add(new Vector2(_quadrants[quadrants[counter]][0].RandomValue(), _quadrants[counter][1].RandomValue()));
        }

        _quadrant = quadrants[3];

        _points.Clear();

        for (int counter = 0; counter < _pointsCount - 1; counter++)
            _points.Add(CalculateBezierCurvePoint(((float)counter) / _pointsCount, controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3]));

        _points.Add(controlPoints[3]);

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

        if (_index >= _pointsCount || (_index + 1 >= _pointsCount && _transform.position.Equals(_points[_pointsCount - 1])))
        {
            CalculateBezierCurvePath();
            _index = 0;
        }

        else
        {

            if (_transform.position.Equals(_points[_index]))
            {
                _maxStep = (int)((_points[_index + 1] - _points[_index]).magnitude * 10) + 1;
                _step = 0;
                _index++;
            }

            else
            {
                if (_step == _maxStep)
                    _transform.position = _points[_index];
                else
                {
                    _transform.position = new Vector2(Mathf.Lerp(_points[_index - 1].x, _points[_index].x, _step / _maxStep), Mathf.Lerp(_points[_index - 1].y, _points[_index].y, _step / _maxStep));
                    _step++;
                }
            }

        }*/
        


    }

}

/*
public class CurveMov : MonoBehaviour
{
    public bool on;

    public float way;
    public float duration;
    public float progress;

    public Vector3 mPoint; //Middle point of the curve

    public Transform target;

    void Start()
    {

        SetMid();

    }

    Vector3 BezierMov(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {

        float u = 1f - t;
        float uu = u * u;
        float tt = t * t;

        Vector3 p = uu * p0;
        p += 2f * u * t * p1;
        p += tt * p2;

        return p;
    }

    public void SetMid()
    {

        mPoint = (target.position + transform.position) / 2;
        mPoint.y += 10f; //Add some height
    }
    /*
    void Update()
    {

        if (on)
        {
            float dist = (BezierMov(way + 0.01f, start, mPoint, target.position) - transform.position).magnitude;
            float f = 0.01f / dist;

            way += speed * f * Time.deltaTime;
            way = Mathf.Clamp01(way);
            transform.position = BezierMov(way, start, mPoint, target.position);
        }
    }
}
*/