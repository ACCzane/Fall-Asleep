using UnityEngine;

public class FoundTankFixedState : IStateBase
{
    private float timeCounter = 0;
    private float fixDuration = 10f;        //需要修10秒
    private SpriteRenderer spr;
    private Transform targetTransform;                      

    public bool fixing;
    public bool finished;

    private Vector3 originPos;

    public void Enter(Enemy enemy)
    {
        //获取组件
        spr = enemy.Spr;
        //获取玩家Transform（追踪玩家）
        targetTransform = enemy.TargetTransform;
        //关闭视野
        enemy.View.gameObject.SetActive(false);

        //以下变量用于作为插值的起始值
        originPos = enemy.transform.position;

        //退出状态置0
        fixing = false;

        //更新夜间状态为疲惫
        enemy.UpdateNightStat(NightStat.Tired);
    }

    public void Exit(Enemy enemy)
    {
        // throw new System.NotImplementedException();
    }

    public void Update(Enemy enemy)
    {
        // Debug.Log(Progress);
        if(fixing){
            //已经在在修电箱了
            timeCounter += Time.deltaTime;
            if(timeCounter > fixDuration){
                //如果到达了修复时间

                //破坏电箱
                targetTransform.GetComponent<Tank>().BreakTank();
                //打开视野
                enemy.View.gameObject.SetActive(true);
                //退出信号
                finished = true;
            }

            return;
        }

        timeCounter += Time.deltaTime;

        Vector3 dir = (targetTransform.position - enemy.transform.position).normalized;
        enemy.transform.Translate(dir * enemy.EnemySpeed*2 * Time.deltaTime, Space.World);
        // spr.color = Color.Lerp(originColor, color_alpha0, timeCounter/timeDuration);
    
        if(Vector3.Distance(targetTransform.position, enemy.transform.position) < 1f){
            Debug.Log(Vector3.Distance(targetTransform.position, enemy.transform.position));
            fixing = true;
        }
    }
}
