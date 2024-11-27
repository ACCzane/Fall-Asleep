using System.Collections.Generic;
using UnityEngine;

public class AlertState : IStateBase
{  

    private bool isLoop;                                //决定是否循环播放Alert

    private Quaternion finalRotation;                   //决定最终旋转
    private Quaternion currentRotation;

    public bool finished = false;                       //退出标志
    private bool isFinalRotation = false;               //退出标志

    private List<float> zAngles;        //决定警戒时会往哪些方向看
    private int currentRotationIndex;      //当前正在执行的旋转
    private int RotationMax;

    public AlertState(){
        //默认情况下的旋转情况
        zAngles = new List<float>();
        zAngles.Add(35);
        zAngles.Add(54);
        zAngles.Add(-38);
        zAngles.Add(-130);
        zAngles.Add(103);
    }

    public AlertState(List<float> zAngles){
        this.zAngles = new List<float>(zAngles);
    }

    public void Enter(Enemy enemy)
    {
        //如果该敌人是巡逻者，Alert不循环播放，有最终位置
        if(enemy is Enemy_Soldior){

            isLoop = false;

            Enemy_Soldior enemy_Soldior = enemy as Enemy_Soldior;
            Vector2 finalTargetDirection = 
                (enemy_Soldior.PosNodes[enemy_Soldior.currentHeadingNodeIndex]
                - new Vector2(enemy.transform.position.x, enemy.transform.position.y)).normalized;
        
            // 计算目标旋转，只需处理Z轴旋转
            float angle = Mathf.Atan2(finalTargetDirection.y, finalTargetDirection.x) * Mathf.Rad2Deg; // 将弧度转换为角度
            finalRotation = Quaternion.Euler(0, 0, angle);     //创建最终目标旋转
        } 
        else if(enemy is Enemy_Guard){

            isLoop = true;

        }


        currentRotationIndex = 0;
        RotationMax = zAngles.Count;

        //更新第一个旋转
        currentRotation = Quaternion.Euler(0,0,zAngles[currentRotationIndex]); 
    }

    public void Exit(Enemy enemy)
    {
        
    }

    public void Update(Enemy enemy)
    {
        if(!isLoop){
            Update_NotLoop(enemy);
        }else{
            Update_Loop(enemy);
        }
        
    }


    // 不循环旋转
    private void Update_NotLoop(Enemy enemy){
        if(finished){return;}

        enemy.View.rotation = Quaternion.RotateTowards(
            enemy.View.rotation,
            currentRotation,
            Time.deltaTime * enemy.RotateSpeed
        );

        if(Quaternion.Angle(enemy.View.rotation, currentRotation) < 0.1f){
            //如果到达了目标旋转
            //切换到下一个旋转方向

            if(isFinalRotation){
                //如果是最终旋转
                //结束标志置true，程序准备退出
                finished = true;
            }

            if(currentRotationIndex+1 == RotationMax){
                //如果超出了警戒的旋转变更数量范围
                //准备旋转到最终方向
                //更新目标旋转
                currentRotation = finalRotation;
                isFinalRotation = true;

                return;
            }

            //更新目标旋转
            currentRotationIndex++;
            currentRotation = Quaternion.Euler(0,0,zAngles[currentRotationIndex]);
        }
    }

    // 循环旋转
    private void Update_Loop(Enemy enemy){

        enemy.View.rotation = Quaternion.RotateTowards(
            enemy.View.rotation,
            currentRotation,
            Time.deltaTime * enemy.RotateSpeed
        );

        if(Quaternion.Angle(enemy.View.rotation, currentRotation) < 0.1f){
            //如果到达了目标旋转
            //切换到下一个旋转方向


            if(currentRotationIndex+1 == RotationMax){
                //如果超出了警戒的旋转变更数量范围
                //旋转回第一个方向
                currentRotationIndex = 0;

                currentRotation = Quaternion.Euler(0,0,zAngles[currentRotationIndex]);

                return;
            }

            //更新目标旋转
            currentRotationIndex++;
            currentRotation = Quaternion.Euler(0,0,zAngles[currentRotationIndex]);
        }
    }
}
