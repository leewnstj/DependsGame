using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points; //waypoint에 쓸 points;
    
    public Vector3[] Points => points; //WayPointEditor에 쓸 points;
    public Vector3 CurrentPosition => _currentPosition;

    private bool _gameStarted;
    private Vector3 _currentPosition;

    void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    void Update()
    {
        
    }

    public Vector3 GetWayPointPosition(int index)
    {
        return CurrentPosition + points[index];
    }

    private void OnDrawGizmos()
    {
        if(!_gameStarted && transform.hasChanged) //gameStarted가 false고, 포지션 값이 바뀌었으면
        {
            _currentPosition = transform.position; //_currentPosition에 현재 포지션 값을 넣는다
        }

        for(int i = 0; i<points.Length; i++) //기즈모 원
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i]+_currentPosition, radius:0.5f); // radius 원 반지름

            if(i < points.Length -1) //기즈모 선
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i+1] + _currentPosition);
            }
        }
    }
}
