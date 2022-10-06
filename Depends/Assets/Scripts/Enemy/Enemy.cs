using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private float moveSpeed = 3.0f;

    public float MoveSpeed { get; set; }
    //[SerializeField] private WayPoint wayPoint;
    public WayPoint wayPoint { get; set; }


    public Vector3 CurrentPointPosition => wayPoint.GetWayPointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;
    private Vector3 _lastPointPosition;

    private EnemyHealth _enemyHealth;
    private SpriteRenderer _spriteRenderer;

    public EnemyHealth EnemyHealth { get; set; }

    private void Start()
    {
        _currentWaypointIndex = 0;
        _enemyHealth = GetComponent<EnemyHealth>();
        EnemyHealth = GetComponent<EnemyHealth>();

        MoveSpeed = moveSpeed;
        _lastPointPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Rotate();

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
            _lastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    private void Rotate()
    {
        if(CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
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
            OnEndReached.Invoke(this);
        }

        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}
