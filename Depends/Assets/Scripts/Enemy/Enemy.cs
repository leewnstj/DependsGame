using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached;

    [SerializeField] private float moveSpeed = 3.0f;

    public float MoveSpeed { get; set; }
    //[SerializeField] private WayPoint wayPoint;
    public WayPoint wayPoint { get; set; }


    public Vector3 CurrentPointPosition => wayPoint.GetWayPointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;

    private EnemyHealth _enemyHealth;

    private void Start()
    {
        _currentWaypointIndex = 0;
        _enemyHealth = GetComponent<EnemyHealth>();

        MoveSpeed = moveSpeed;
    }

    private void Update()
    {
        Move();

        if(CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        //Vector3 currenPosition = wayPoint.GetWayPointPosition(_currentWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if(distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = wayPoint.Points.Length - 1;

        //Enemy가 끝까지 도착 안했다면
        if(_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++; //증가
        }
        //Enemy가 끝까지 도착 했다면
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        if(OnEndReached != null)
        {
            OnEndReached.Invoke();
        }

        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}
