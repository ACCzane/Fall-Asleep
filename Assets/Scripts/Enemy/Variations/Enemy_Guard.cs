using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Enemy_Guard : Enemy
{

    [SerializeField] private RoundingView roundingView;

    private void Start()
    {
        stateMachine = new EnemyStateMachine(this);

        Initialize();
    }

    private void Update()
    {
        stateMachine.Update(); // 更新状态
        // Debug.Log(stateMachine.currentState);

        //入口状态：Alert(Loop)

        //出口状态
        if(stateMachine.currentState is FoundPlayerState){
            //如果目前正在“找到玩家”的状态
            if((stateMachine.currentState as FoundPlayerState).finished){
                //如果该状态进度条已满(动作执行完毕)
                
                //执行攻击动画
                anim.SetBool("IsRight", (stateMachine.currentState as FoundPlayerState).isFacingRight);
                anim.SetTrigger("Attack");
                // Destroy(gameObject);
                
            }
        }
    }

    public void Initialize(){
        stateMachine.ChangeState(new AlertState(roundingView.eulerAngles_z));
    }

    // public void TargetPlayer(Transform transform){
    //     PlayerTransform = transform;
    // }
}
