using System;
using UnityEngine;

public static class EventHandler
{
    public static Action Hide;
    public static void CallHide(){
        Hide?.Invoke();
    }

    public static Action Mess;
    public static void CallMess(){
        Mess?.Invoke();
    }

    public static Action<Transform> FindPlayer;
    public static void CallAttack(Transform targetTransform){

    }

    //镜头红晕、玩家血量下降
    public static Action Attack;
    public static void CallAttack(){
        Debug.Log("Attack!");
    }
}
