using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button quitButton;

    private void OnEnable() {
        
        quitButton.onClick.AddListener(QuitGame);
    }
    
    public void QuitGame(){
        Application.Quit();
    }
}
