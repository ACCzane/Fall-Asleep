using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TimeManager timeManager;

    private void OnEnable() {
        
        
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

        Debug.Log("StartDay!");

        playerControl.enabled = true;
        timeManager.StartCount();
        uiManager.TurnCountdownText();
    }

}
