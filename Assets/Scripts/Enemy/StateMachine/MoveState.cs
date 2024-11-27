using System.Collections.Generic;
using UnityEngine;

public class MoveState : IStateBase
{
    private Vector2 targetPos;
    
    private Enemy_Soldior enemy_Soldior;            //只有士兵会巡逻

    private Vector2 lastPosition;                   // 存储上一个位置，方便对准旋转
    private Vector2 currentPosition;                //当前位置，方便对准旋转
    private Vector2 currentDir;                     //当前向量

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

        // Debug.Log(enemy_Soldior.currentHeadingNodeIndex + " + " + enemy_Soldior.reverse + " = " + (enemy_Soldior.currentHeadingNodeIndex+enemy_Soldior.reverse));
        enemy_Soldior.currentHeadingNodeIndex += enemy_Soldior.reverse;
    }

    public void Update(Enemy enemy)
    {

        if(targetPos == null){return;}

        // Debug.Log(targetPos);

        enemy.transform.position = Vector2.MoveTowards(
            enemy.transform.position,
            targetPos,
            enemy.EnemySpeed * Time.deltaTime
        );

        currentPosition = enemy.transform.position;
        currentDir = (currentPosition - lastPosition).normalized;

        Debug.Log(currentDir);

        RotateTowards(enemy.View, currentDir, enemy.RotateSpeed * 2);

        lastPosition = currentPosition;
    }

    private void RotateTowards(Transform rotateTransform, Vector2 targetDirection, float rotateSpeed){
        
        // 计算目标旋转，只需处理Z轴旋转
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // 将弧度转换为角度
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle); // 创建目标旋转

        rotateTransform.rotation = Quaternion.RotateTowards(
            rotateTransform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );
    }


}
