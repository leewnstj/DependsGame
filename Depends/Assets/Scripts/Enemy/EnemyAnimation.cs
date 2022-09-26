using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }    

    private void PlayDieAnimation()
    {
        _animator.SetTrigger("Die");
    }

    private float GetCurrentAnimationLength()
    {
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() + 0.5f);
        _enemy.ResumMovement();
    }

    private void EnemyHit(Enemy enemy)
    {
        if(_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
    }
}
