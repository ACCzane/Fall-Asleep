using UnityEngine;

/// <summary>
/// 该状态下，敌人快速接近玩家，并执行攻击
/// </summary>
public class FoundPlayerState : IStateBase
{
    private float timeCounter = 0;
    private SpriteRenderer spr;
    private Transform playerTransform;                      //猎杀时刻！

    public bool finished;
    public bool isFacingRight;                              //用于给动画穿参数

    private Color originColor;
    private Color color_alpha0;
    private Vector3 originPos;

    public void Enter(Enemy enemy)
    {
        //获取组件
        spr = enemy.Spr;
        //获取玩家Transform（追踪玩家）
        playerTransform = enemy.TargetTransform;
        //关闭视野
        enemy.View.gameObject.SetActive(false);

        //以下变量用于作为插值的起始值
        originColor = spr.color;
        color_alpha0 = new Vector4(spr.color.r, spr.color.g, spr.color.b, 0);
        originPos = enemy.transform.position;

        enemy.Anim.SetBool("IsRight", isFacingRight);
        enemy.Anim.SetBool("IsAttack", true);

        enemy.PlayAttackSound();
    }

    public void Exit(Enemy enemy)
    {
        
    }

    public void Update(Enemy enemy)
    {
        // Debug.Log(Progress);

        timeCounter += Time.deltaTime;

        Vector3 dir = (playerTransform.position - enemy.transform.position).normalized;
        enemy.transform.Translate(dir * enemy.EnemySpeed*4 * Time.deltaTime, Space.World);
        //测试为4倍速最合适

        isFacingRight = (playerTransform.position - enemy.transform.position).x > 0 ? true : false;
        enemy.Anim.SetBool("IsRight", isFacingRight);

        // spr.color = Color.Lerp(originColor, color_alpha0, timeCounter/timeDuration);
    
        if(Vector3.Distance(playerTransform.position, enemy.transform.position) < 0.5f){
            //经测试，2f的值刚好
            // Debug.Log(Vector3.Distance(playerTransform.position, enemy.transform.position));
            finished = true;
            
        }
    }
}
