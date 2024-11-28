using System;
using UnityEngine;

public static class EventHandler
{
    public static Action<InteractType> Interact;
    public static void CallInteract(InteractType interactType){
        Interact?.Invoke(interactType);
    }

    public static Action Hide;
    public static void CallHide(){
        Hide?.Invoke();
    }

    public static Action Sleep;
    public static void CallSleep(){
        Sleep?.Invoke();
    }

    public static Action Attack;
    public static void CallAttack(){
        Attack?.Invoke();
    }

    public static Action<int> Countdown;
    public static void CallCountdown(int countdownValue){
        Countdown?.Invoke(countdownValue);
    }

    public static Action NightFall;
    public static void CallNightFall(){
        NightFall?.Invoke();
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
