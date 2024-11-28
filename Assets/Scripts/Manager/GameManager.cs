using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private LightManager lightManager;

    private void OnEnable() {
        EventHandler.NightFall += StartNight;
    }

    private void OnDisable() {
        EventHandler.NightFall -= StartNight;
    }

    private void Start() {
        Initialize();
        
    }
    
    public void QuitGame(){
        Application.Quit();
    }

    public void Initialize(){
        enemyManager.GenerateEnemySoldiors_Day();
    }

    public void StartDay(){

        playerControl.enabled = true;
        timeManager.StartCount();
        uiManager.TurnCountdownText(true);      //打开倒计时，默认关闭
        lightManager.TurnOnGlobalLight();   //全局光照Intensity调整到0.3
    }

    private void StartNight()
    {
        uiManager.TurnCountdownText(false);      //关闭倒计时
        lightManager.TurnOffGlobalLight();  //全局光照Intensity调整到0
        enemyManager.GenerateEnemySoldiors_Night();     //晚上重置敌人

        //先暂停时间
    }

}
