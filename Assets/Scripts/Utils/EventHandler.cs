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

    public static Action UseItemInHand;
    public static void CallUseItemInHand(){
        UseItemInHand?.Invoke();
    }

    public static Action GainCoin;
    public static void CallGainCoin(){
        GainCoin?.Invoke();
    }

    // public static Action Damage;
    // public static void CallDamage(){
    //     Damage?.Invoke();
    // }


    public static Action<int> Countdown;
    public static void CallCountdown(int countdownValue){
        Countdown?.Invoke(countdownValue);
    }

    public static Action NightFall;
    public static void CallNightFall(){
        NightFall?.Invoke();
    }

}
