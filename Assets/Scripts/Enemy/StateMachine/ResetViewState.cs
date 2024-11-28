using UnityEngine;

/// <summary>
/// 在RotateState和MoveState之间重定位视角（三角形）旋转的状态
/// </summary>
public class ResetViewState : IStateBase
{

    public Quaternion TargetRotation{get; private set;}

    private Enemy_Soldior enemy_Soldior;

    public void Enter(Enemy enemy)
    {
        enemy_Soldior = enemy as Enemy_Soldior;

        Debug.Log(enemy_Soldior + " " + enemy_Soldior.currentHeadingNodeIndex + " " + enemy_Soldior.PosNodes.Count);

        Vector2 targetDirection = 
            (enemy_Soldior.PosNodes[enemy_Soldior.currentHeadingNodeIndex]
            - new Vector2(enemy_Soldior.transform.position.x, enemy_Soldior.transform.position.y)).normalized;
        
        // 计算目标旋转，只需处理Z轴旋转
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // 将弧度转换为角度
        TargetRotation = Quaternion.Euler(0, 0, angle); // 创建目标旋转

    }

    public void Exit(Enemy enemy)
    {
        
    }

    public void Update(Enemy enemy)
    {
        enemy_Soldior.View.rotation = Quaternion.RotateTowards(
            enemy_Soldior.View.rotation,
            TargetRotation,
            Time.deltaTime * enemy_Soldior.RotateSpeed
        );
    }
}
