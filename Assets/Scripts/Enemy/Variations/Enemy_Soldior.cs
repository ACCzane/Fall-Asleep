using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Enemy_Soldior : Enemy
{

    #region 移动
    [HideInInspector]public int currentHeadingNodeIndex = 0;     //希望MoveState能改变它
    [HideInInspector]public List<Vector2> PosNodes{get; private set;}
    public int reverse = 1;                                      //1代表正向，-1代表逆向，初始为正
    
    #endregion

    #region 重生相关
    private EnemySpawner enemySpawner;
    #endregion

    private void Start()
    {
        //Bug：如果这里不显示声明1，无论MoveState脚本里对其修改，或是提前在变量里定义，都没用，都会一直是0
        reverse = 1;
    }

    private void OnEnable() {
        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        EventHandler.NightFall -= OnNightFall;
    }

    private void OnNightFall()
    {
        stateMachine.currentState.Exit(this);
        ChangeState(EnemyState.Move);
    }

    private void Update()
    {
        stateMachine.Update(); // 更新状态
        // Debug.Log(stateMachine.currentState);
        //入口状态
        if(stateMachine.currentState is ResetViewState resetViewState
            && Quaternion.Angle(view.rotation , resetViewState.TargetRotation) < 0.1f){
                //如果重定位视角结束，对齐了下一次行动的方向
                //则进入移动状态
                //目前是状态机第一个动作，后续应当不会进入
                ChangeState(EnemyState.Move);
        }

        if(stateMachine.currentState is MoveState
            && Vector2.Distance(transform.position, PosNodes[currentHeadingNodeIndex])<0.1f){
                //如果目前正在巡逻且到达了目标点
                //则概率进入警戒状态
                
                float randomValue = Random.Range(0f, 1f);
                if(randomValue<0.3f)
                {
                    ChangeState(EnemyState.Alert);
                }
                else
                {
                    ChangeState(EnemyState.Move);
                }
        }

        if(stateMachine.currentState is AlertState){
            //如果目前正在警戒状态
            if((stateMachine.currentState as AlertState).finished){
                //如果警戒状态时间结束
                //则进入巡逻状态
                ChangeState(EnemyState.Move);
            }
        }

        //出口状态, 在SightCheck脚本中执行状态的更新（发现玩家）
        if(stateMachine.currentState is FoundPlayerState){
            //如果目前正在“找到玩家”的状态
            if((stateMachine.currentState as FoundPlayerState).finished){
                //如果该状态进度条已满(动作执行完毕)
                
                //通知Spawner重新生成一个（会有等待时间）

                // enemySpawner.EnemyRebirth();
                
                // throw new System.NotImplementedException();
                GenImpulse();

                Attack(targetTransform.GetComponent<PlayerStat>());

                // EnemyManager.Singleton.RemoveEnemy(enemy);

                Anim.SetBool("IsAttack", false);

                DespawnSelf();
                
                //执行攻击动画
                // anim.SetBool("IsRight", (stateMachine.currentState as FoundPlayerState).isFacingRight);
                // anim.SetTrigger("Attack");
                // Destroy(gameObject);
                
            }
        }

        if(stateMachine.currentState is FoundTankFixedState){
            //如果目前正在“发现电箱被修复”的状态
            if((stateMachine.currentState as FoundTankFixedState).finished){
                //如果该状态进度条已满（动作执行完毕）

                ChangeState(EnemyState.Move);
            }
        }
    }

    private void SetMovePath(MovePath movePath){
        this.PosNodes = movePath.GetPos();
    }

    public void Initialize(MovePath movePath, EnemySpawner enemySpawner){

        reverse = 1;

        this.enemySpawner = enemySpawner;

        SetMovePath(movePath);

        stateMachine = new EnemyStateMachine(this);

        transform.position = PosNodes[0];
        
        currentHeadingNodeIndex = 0;                //currentHeadingNode决定了往哪走，在MoveState中的Enter读取本文件的currentHeadingNode
        ChangeState(EnemyState.ResetView); // 初始状态
    }

    public void Initialize(){
        reverse = 1;
        transform.position = PosNodes[0];
        
        currentHeadingNodeIndex = 0;
        ChangeState(EnemyState.ResetView);
    }

    public override void DespawnSelf(){
        enemySpawner.DespawnEnemy();
    }

}
