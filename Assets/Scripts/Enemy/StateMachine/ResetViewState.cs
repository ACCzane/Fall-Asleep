using UnityEngine;

/// <summary>
/// 在RotateState和MoveState之间重定位视角（三角形）旋转的状态
/// </summary>
public class ResetViewState : IStateBase
{

    public Quaternion TargetRotation{get; private set;}

    public void Enter(Enemy enemy)
    {
        Vector2 targetDirection = 
            (enemy.PosNodes[enemy.currentHeadingNodeIndex]
            - new Vector2(enemy.transform.position.x, enemy.transform.position.y)).normalized;
        
        // 计算目标旋转，只需处理Z轴旋转
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // 将弧度转换为角度
        TargetRotation = Quaternion.Euler(0, 0, angle); // 创建目标旋转


        Debug.Log(TargetRotation.eulerAngles);
        Debug.Log(enemy.View.rotation.eulerAngles);
    }

    public void Exit(Enemy enemy)
    {
        
    }

    public void Update(Enemy enemy)
    {
        enemy.View.rotation = Quaternion.RotateTowards(
            enemy.View.rotation,
            TargetRotation,
            Time.deltaTime * enemy.RotateSpeed
        );
    }
}
