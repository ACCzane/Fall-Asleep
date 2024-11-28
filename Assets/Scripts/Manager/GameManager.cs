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
        enemyManager.GenerateEnemies_Day();
    }

    public void StartDay(){

        playerControl.enabled = true;
        timeManager.StartCount();
        uiManager.TurnCountdownText();
    }

    private void StartNight()
    {
        
    }

}
