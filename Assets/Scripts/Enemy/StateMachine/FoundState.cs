using UnityEngine;

/// <summary>
/// 该状态下，敌人“上浮”，逐渐消失，玩家相机逐渐出现暗角，随后玩家扣血，相机震动
/// </summary>
public class FoundState : IStateBase
{
    private float timeDuration = 2f;
    private float timeCounter = 0;
    private SpriteRenderer spr;
    private Transform playerTransform;                      //猎杀时刻！

    public float Progress => timeCounter/timeDuration;      //当前的进度，0代表开始，1代表结束

    private Color originColor;
    private Color color_alpha0;
    private Vector3 originPos;

    public void Enter(Enemy enemy)
    {
        //获取组件
        spr = enemy.Spr;
        //获取玩家Transform（追踪玩家）
        playerTransform = enemy.PlayerTransform;
        //关闭视野
        enemy.View.gameObject.SetActive(false);

        //以下变量用于作为插值的起始值
        originColor = spr.color;
        color_alpha0 = new Vector4(spr.color.r, spr.color.g, spr.color.b, 0);
        originPos = enemy.transform.position;
    }

    public void Exit(Enemy enemy)
    {
        // throw new System.NotImplementedException();
    }

    public void Update(Enemy enemy)
    {
        // Debug.Log(Progress);

        timeCounter += Time.deltaTime;

        enemy.transform.position = Vector3.Lerp(originPos, playerTransform.position, timeCounter/timeDuration);
        spr.color = Color.Lerp(originColor, color_alpha0, timeCounter/timeDuration);
    }
}
