using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float timeCounter;
    [SerializeField] private float timeMax;
    private bool isCounting = false;

    private int countdown;


    private void Update() {
        
        if(isCounting){
            timeCounter -= Time.deltaTime;
            UIManager.Singleton.UpdateTimecountBar(timeCounter/timeMax);
            // countdown = GetCountdown();
            if(timeCounter <= 0){
                
                //劣势进入黑夜
                
                EventHandler.CallNightFall();

                GameManager.Singleton.SetPlayerHealthHalf();

                isCounting = false;
                return;
            }
            // if(previousCountdown > countdown){
            //     //如果倒计时值变化，call event
            //     EventHandler.CallCountdown(countdown);
            // }
        }
    }

    public void StartCount(){
        // EventHandler.CallCountdown((int)timeMax);

        timeCounter = timeMax;
        isCounting = true;
    }

    // public int GetCountdown(){
    //     int countdown = Mathf.CeilToInt(timeMax - timeCounter);
    //     return countdown;
    // }

    public void ShutCountdown(){
        isCounting = false;
    }
}
