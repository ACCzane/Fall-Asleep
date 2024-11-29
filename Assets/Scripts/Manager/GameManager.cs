using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Singleton;

    [SerializeField] private PlayerControl playerControl;

    [SerializeField] private GameObject DespawnInNight;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private LightManager lightManager;

    [SerializeField] private GameObject player;

    private bool isNight;

    private void Awake() {
        if(Singleton == null)
            Singleton = this;
    }

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
        lightManager.TurnOnGlobalLight();   //全局光照Intensity调整到1

        isNight = false;
    }

    private void StartNight()
    {

        //播放进入黑夜的动画
        //TODO

        //晚上不会出现的物体被删除
        Destroy(DespawnInNight);

        //动画结束后
        uiManager.TurnCountdownText(false);      //关闭倒计时面板
        timeManager.ShutCountdown();             //关闭倒计时逻辑
        lightManager.TurnOffGlobalLight();  //全局光照Intensity调整到0
        enemyManager.GenerateEnemySoldiors_Night();     //晚上重置敌人

        player.GetComponentInChildren<Animator>().SetBool("IsGhost", true);   //修改玩家为幽灵，Animator初始默认情况为非幽灵
    
        isNight = true;
    }

    public bool IsNight(){
        return isNight;
    }

}
