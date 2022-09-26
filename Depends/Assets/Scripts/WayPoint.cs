using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points; //waypoint�� �� points;
    
    public Vector3[] Points => points; //WayPointEditor�� �� points;
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
        if(!_gameStarted && transform.hasChanged) //gameStarted�� false��, ������ ���� �ٲ������
        {
            _currentPosition = transform.position; //_currentPosition�� ���� ������ ���� �ִ´�
        }

        for(int i = 0; i<points.Length; i++) //����� ��
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i]+_currentPosition, radius:0.5f); // radius �� ������

            if(i < points.Length -1) //����� ��
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i+1] + _currentPosition);
            }
        }
    }
}
