using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float duration;

    public void OnEnter(Enemy enemy)
    {
        timer = 0f;
        duration = Random.Range(3f, 5f);
    }

    public void OnExecute(Enemy enemy)
    {
        if(enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
                return;
            }
        }

        enemy.Moving();

        timer += Time.deltaTime;
        if(timer > duration)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }

}
