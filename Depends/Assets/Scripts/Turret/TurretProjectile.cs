using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPos;
    private ObjectPooler _pooler;

    private Projectile _currentProjectileLoaded;
    private Turret _turret;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _turret = GetComponent<Turret>();

        LoadProjectile();
    }

    private void Update()
    {
        if(IsTurretEmpty())
        {
            LoadProjectile();
        }

        if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null &&
            _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
        {
            _currentProjectileLoaded.transform.parent = null;
            _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
        }
    }

    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPos.position;
        newInstance.transform.SetParent(projectileSpawnPos);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this;

        newInstance.SetActive(true);
    }

    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }

    private bool IsTurretEmpty()
    {
        return _currentProjectileLoaded == null;
    }
}
