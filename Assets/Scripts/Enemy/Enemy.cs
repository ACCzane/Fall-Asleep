using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStateMachine stateMachine;

    [SerializeField] private SpriteRenderer spr;
    public SpriteRenderer Spr{
        get{
            return spr;
        } 
        private set{
            spr = value;
        }
    }

    [Header("参数")]
    [SerializeField] private float enemySpeed = 5f;
    public float EnemySpeed{
        get{return enemySpeed;}
        private set{enemySpeed = value;}
    }

    #region 移动
    [HideInInspector]public int currentHeadingNodeIndex = 0;     //希望MoveState能改变它
    [HideInInspector]public List<Vector2> PosNodes{get; private set;}
    public int reverse = 1;                                      //1代表正向，-1代表逆向，初始为正
    
    #endregion

    #region 视线转动
    [SerializeField] private Transform view;
    public Transform View{
        get{
            return view;
        }
        set{
            view = value;
        }
    }
    [SerializeField] private RoundingView roundingView;
    [SerializeField] private float rotateSpeed = 20f;            //每秒旋转的角度值
    public float RotateSpeed{
        get{
            return rotateSpeed;
        }
        private set{
            rotateSpeed = value;
        }
    }
    #endregion

    #region 找到玩家相关
    public Transform PlayerTransform{get; private set;}
    #endregion

    #region 重生相关
    private EnemySpawner enemySpawner;
    #endregion

    private void Start()
    {
        //Bug：如果这里不显示声明1，无论MoveState脚本里对其修改，或是提前在变量里定义，都没用，都会一直是0
        reverse = 1;
    }

    private void Update()
    {
        stateMachine.Update(); // 更新状态
        Debug.Log(currentHeadingNodeIndex);
        Debug.Log(stateMachine.currentState);
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
                //则进入警戒状态
                ChangeState(EnemyState.Alert);
        }

        if(stateMachine.currentState is AlertState){
            //如果目前正在警戒状态
            if((stateMachine.currentState as AlertState).finished){
                //如果警戒状态时间结束
                //则进入巡逻状态
                ChangeState(EnemyState.Move);
            }
        }

        //出口状态
        if(stateMachine.currentState is FoundState){
            //如果目前正在“找到玩家”的状态
            if((stateMachine.currentState as FoundState).Progress > 0.99f){
                //如果该状态进度条已满(动作执行完毕)
                
                //通知Spawner重新生成一个（会有等待时间）
                enemySpawner.EnemyRebirth();
                
                //销毁该敌人
                Destroy(gameObject);
                
            }
        }
    }

    private void SetMovePath(MovePath movePath){
        this.PosNodes = movePath.GetPos();
    }

    public void Initialize(MovePath movePath, EnemySpawner enemySpawner){

        this.enemySpawner = enemySpawner;

        SetMovePath(movePath);

        transform.position = PosNodes[0];

        stateMachine = new EnemyStateMachine(this);
        
        currentHeadingNodeIndex = 1;                //currentHeadingNode决定了往哪走，在MoveState中的Enter读取本文件的currentHeadingNode
        ChangeState(EnemyState.ResetView); // 初始状态
    }

    public void ChangeState(EnemyState enemyState){
        if(enemyState == EnemyState.Move){
            stateMachine.ChangeState(new MoveState());
            return;
        }
        if(enemyState == EnemyState.Alert){
            stateMachine.ChangeState(new AlertState());
            return;
        }
        if(enemyState == EnemyState.ResetView){
            stateMachine.ChangeState(new ResetViewState());
            return;
        }
        if(enemyState == EnemyState.Found){
            stateMachine.ChangeState(new FoundState());
            return;
        }
    }

    public void TargetPlayer(Transform transform){
        PlayerTransform = transform;
    }

#region CinemachineImpulse
    [SerializeField] private  CinemachineImpulseSource impulseSource;

    public void GenImpulse(){
        impulseSource?.GenerateImpulse();
    }
#endregion

}
