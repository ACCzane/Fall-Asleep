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
}
