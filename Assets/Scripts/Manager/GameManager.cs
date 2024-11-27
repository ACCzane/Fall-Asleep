using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;

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


}
