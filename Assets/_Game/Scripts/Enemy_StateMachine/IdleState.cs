using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float duration;

    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        duration = Random.Range(1f, 3f);
        timer = 0f;
        
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if(timer > duration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
