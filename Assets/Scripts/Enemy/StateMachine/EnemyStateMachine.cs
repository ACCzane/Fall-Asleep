using System;
using UnityEngine;

public class EnemyStateMachine
{
    public IStateBase currentState;
    private Enemy enemy;

    public EnemyStateMachine(Enemy enemy){
        this.enemy = enemy;
    }

    public void ChangeState(IStateBase newState){
        if (currentState != null)
        {
            currentState.Exit(enemy); // 退出当前状态
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(enemy); // 进入新状态
        }
    }

    // 更新方法，通常在每帧调用
    public void Update()
    {
        currentState?.Update(enemy);
    }

}
