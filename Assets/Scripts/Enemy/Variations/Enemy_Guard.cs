using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Enemy_Guard : Enemy
{
    // private EnemyStateMachine stateMachine;

    // [SerializeField] private SpriteRenderer spr;
    // public SpriteRenderer Spr{
    //     get{
    //         return spr;
    //     } 
    //     private set{
    //         spr = value;
    //     }
    // }


    // #region 重生相关
    // private EnemySpawner enemySpawner;
    // #endregion

    private void Start()
    {

    }

    private void Update()
    {
        stateMachine.Update(); // 更新状态
        Debug.Log(stateMachine.currentState);
        //入口状态
        

        //出口状态
        if(stateMachine.currentState is FoundState){
            //如果目前正在“找到玩家”的状态
            if((stateMachine.currentState as FoundState).Progress > 0.99f){
                //如果该状态进度条已满(动作执行完毕)
                //销毁该敌人
                Destroy(gameObject);
                
            }
        }
    }

    public void Initialize(MovePath movePath, EnemySpawner enemySpawner){

        
        ChangeState(EnemyState.Alert); // 初始状态
    }

    public void TargetPlayer(Transform transform){
        PlayerTransform = transform;
    }
}
