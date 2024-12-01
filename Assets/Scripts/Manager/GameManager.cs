using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int currentBuildIndex;

    public static GameManager Singleton;

    [SerializeField] private PlayerControl playerControl;

    [SerializeField] private GameObject[] DespawnInNight;
    [SerializeField] private GameObject[] SpawnInNight;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private PlayerEnterLevel entry;
    // [SerializeField] private Exit exit;
    [SerializeField] private PlayableDirector director;

    [SerializeField] private GameObject player;

    private bool isNight;

    private void Awake() {
        if(Singleton == null)
            Singleton = this;
    }

    private void OnEnable() {
        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        EventHandler.NightFall -= OnNightFall;
    }

    private void Start() {
        Initialize();
        
        PlayerStartEnter();
    }
    
    public void QuitGame(){
        Application.Quit();
    }

    public void Initialize(){
        enemyManager.GenerateEnemySoldiors_Day();
    }

    public void StartDay(){
        //打开所有白天物品，关闭所有晚上物品
        RespawnItems(true);

        playerControl.enabled = true;
        timeManager.StartCount();
        // uiManager.TurnCountdownText(true);      //打开倒计时，默认关闭
        // lightManager.TurnOnGlobalLight();   //全局光照Intensity调整到1

        isNight = false;
    }

    private void OnNightFall()
    {
        StartCoroutine(NightFallAsync(1f,1f,1f));
    }

    private IEnumerator NightFallAsync(float fadein, float fadeout, float duration){

        //剥夺玩家控制
        playerControl.enabled = false;

        //播放进入黑夜的动画
        //调用UIManager
        yield return uiManager.FadeIn(fadein,duration);

        //如果玩家未睡觉，先移动至入口处
        if(!player.GetComponent<Sleep>().IsSleeping){
            entry.SetPlayerPosToEntry();
        }
        

        RespawnItems(false);
        

        // uiManager.TurnCountdownText(false);      //关闭倒计时面板
        timeManager.ShutCountdown();             //关闭倒计时逻辑
        lightManager.TurnOffGlobalLight();  //全局光照Intensity调整到0
        enemyManager.GenerateEnemySoldiors_Night();     //晚上重置敌人

        playerControl.enabled = true;            //归还玩家控制
        playerControl.GetComponent<Hide>().EnablePlayerSpr();   //打开玩家Spr
        

        player.GetComponentInChildren<Animator>().SetBool("IsGhost", true);   //修改玩家为幽灵，Animator初始默认情况为非幽灵

        isNight = true;

        yield return uiManager.FadeOut(fadeout);

        if(!player.GetComponent<Sleep>().IsSleeping){
            entry.StartPlayerEnterLevel();
        }
    }


    public void RespawnItems(bool isDay){
        if(!isDay)
        {
            foreach (var item in DespawnInNight)
            {
                item.SetActive(false);
            }
            foreach (var item in SpawnInNight)
            {
                item.SetActive(true);
            }
        }else{
            foreach (var item in DespawnInNight)
            {
                item.SetActive(true);
            }
            foreach (var item in SpawnInNight)
            {
                item.SetActive(false);
            }
        }
    }

    public bool IsNight(){
        return isNight;
    }

    public void Fail(){
        StartCoroutine(PlayerDie());
    }

    public void Success(){
        ReturnMenu();
    }

    public void PlayerStartEnter(){
        playerControl.transform.position = entry.EntryPos_1;
        director.Play();        //包含了emit，接收后先执行PlayerEnter, 再执行StartDay
    }   

    public void ReturnMenu(){


        SceneManager.LoadSceneAsync(0);
        // SceneManager.LoadSceneAsync(index);
    }

    public void RestartGame(){
        uiManager.TurnSettingPanel(false);
        SceneManager.LoadScene(currentBuildIndex);

    }

    public IEnumerator PlayerDie(){

        playerControl.enabled = false;

        //渐出
        // yield return uiManager.FadeIn(1f,1f);
        //打开游戏失败UI
        uiManager.TurnSettingPanel(true);
        //关闭玩家实体，放置在UI背后敌人继续创玩家..
        playerControl.gameObject.SetActive(false);
        //渐入
        // yield return uiManager.FadeOut(1f);

        // Debug.Log("fadeout");

        yield return null;
    }

    public void SetPlayerHealthHalf(){
        playerControl.GetComponent<PlayerStat>().Punish();
    }

}
