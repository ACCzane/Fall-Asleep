using System.Collections.Generic;
using UnityEngine;

public class MoveState : IStateBase
{
    private Vector2 targetPos;
    

    public void Enter(Enemy enemy)
    {
        if(enemy.PosNodes == null){
            Debug.LogError("enemyPosNodes is null!");
            return;
        }
        targetPos = enemy.PosNodes[enemy.currentHeadingNodeIndex];
    }

    public void Exit(Enemy enemy)
    {
        if(enemy.currentHeadingNodeIndex == enemy.PosNodes.Count-1){enemy.reverse = -1;}
        else if(enemy.currentHeadingNodeIndex == 0){enemy.reverse = 1;}

        Debug.Log(enemy.currentHeadingNodeIndex + " + " + enemy.reverse + " = " + (enemy.currentHeadingNodeIndex+enemy.reverse));
        enemy.currentHeadingNodeIndex += enemy.reverse;
    }

    public void Update(Enemy enemy)
    {

        if(targetPos == null){return;}

        Debug.Log(targetPos);

        enemy.transform.position = Vector2.MoveTowards(
            enemy.transform.position,
            targetPos,
            enemy.EnemySpeed * Time.deltaTime
        );
    }
}
