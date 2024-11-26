using System;
using UnityEngine;

public static class EventHandler
{
    public static Action<InteractType> Interact;
    public static void CallInteract(InteractType interactType){
        Interact?.Invoke(interactType);
    }

    public static Action Attack;
    public static void CallAttack(){
        Attack?.Invoke();
    }

    public static Action Sleep;
    public static void CallSleep(){
        Sleep?.Invoke();
    }
}
