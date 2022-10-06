using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange = 3.0f;

    private bool _gameStarted;

    private List<Enemy> _enemies;

    public Enemy CurrentEnemyTarget { get; set; }

    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Enemy>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackRange;
        }
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy newEnemy = collision.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }

    private void GetCurrentEnemyTarget()
    {
        if(_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }
        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }
        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }
}
