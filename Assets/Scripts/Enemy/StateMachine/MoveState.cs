using System.Collections.Generic;
using UnityEngine;

public class MoveState : IStateBase
{
    private Vector2 targetPos;
    
    private Enemy_Soldior enemy_Soldior;

    public void Enter(Enemy enemy)
    {

        if(enemy is Enemy_Soldior){
            enemy_Soldior = enemy as Enemy_Soldior;
        }

        if(enemy_Soldior.PosNodes == null){
            Debug.LogError("enemyPosNodes is null!");
            return;
        }
        targetPos = enemy_Soldior.PosNodes[enemy_Soldior.currentHeadingNodeIndex];
    }

    public void Exit(Enemy enemy)
    {
        if(enemy_Soldior.currentHeadingNodeIndex == enemy_Soldior.PosNodes.Count-1){enemy_Soldior.reverse = -1;}
        else if(enemy_Soldior.currentHeadingNodeIndex == 0){enemy_Soldior.reverse = 1;}

        Debug.Log(enemy_Soldior.currentHeadingNodeIndex + " + " + enemy_Soldior.reverse + " = " + (enemy_Soldior.currentHeadingNodeIndex+enemy_Soldior.reverse));
        enemy_Soldior.currentHeadingNodeIndex += enemy_Soldior.reverse;
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
