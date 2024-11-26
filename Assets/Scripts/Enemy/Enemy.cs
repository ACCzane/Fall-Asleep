using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStateMachine stateMachine;

    [Header("参数")]
    [SerializeField] private float enemySpeed = 5f;
    public float EnemySpeed{
        get{return enemySpeed;}
        private set{enemySpeed = value;}
    }

    #region 移动
    [HideInInspector]public int currentHeadingNodeIndex = 0;     //希望MoveState能改变它
    [HideInInspector]public List<Vector2> PosNodes{get; private set;}
    [HideInInspector]public int reverse;                        //1代表正向，-1代表逆向
    
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
    #endregion

    private void Start()
    {
        
    }

    private void Update()
    {
        stateMachine.Update(); // 更新状态
        
        if(stateMachine.currentState is MoveState
            && Vector2.Distance(transform.position, PosNodes[currentHeadingNodeIndex])<0.1f){
                //如果目前正在巡逻且到达了目标点
                //则进入警戒状态
                stateMachine.ChangeState(new AlertState());
        }

        if(stateMachine.currentState is ResetViewState resetViewState
            && Quaternion.Angle(view.rotation , resetViewState.TargetRotation) < 0.1f){
                //如果重定位视角结束，对齐了下一次行动的方向
                //则进入移动状态
                stateMachine.ChangeState(new MoveState());
        }

        if(stateMachine.currentState is AlertState){
            //如果目前正在警戒状态
            if((stateMachine.currentState as AlertState).finished){
                //如果警戒状态时间结束
                //则进入巡逻状态
                stateMachine.ChangeState(new MoveState());
            }
        }
    }

    private void SetMovePath(MovePath movePath){
        this.PosNodes = movePath.GetPos();
    }

    public void Initialize(MovePath movePath){
        SetMovePath(movePath);

        stateMachine = new EnemyStateMachine(this);
        
        currentHeadingNodeIndex = 1;                //currentHeadingNode决定了往哪走，在MoveState中的Enter读取本文件的currentHeadingNode
        stateMachine.ChangeState(new ResetViewState()); // 初始状态
    }

}
