using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private TimePeriod time;
    private float timeCounter;
    [SerializeField] private float timeMax;
    private bool isCounting = false;

    private int previousCountdown;
    private int countdown;

    

    private void Update() {
        
        if(isCounting){
            timeCounter += Time.deltaTime;
            countdown = GetCountdown();
            if(timeCounter > timeMax){
                
                //进入黑夜
                EventHandler.CallNightFall();
                isCounting = false;
                return;
            }
            if(previousCountdown > countdown){
                //如果倒计时值变化，call event
                EventHandler.CallCountdown(countdown);
            }
            previousCountdown = countdown;
        }
    }

    public void StartCount(){
        EventHandler.CallCountdown((int)timeMax);

        timeCounter = 0;
        isCounting = true;
    }

    public int GetCountdown(){
        int countdown = Mathf.CeilToInt(timeMax - timeCounter);
        return countdown;
    }

    public void ShutCountdown(){
        isCounting = false;
    }
}
