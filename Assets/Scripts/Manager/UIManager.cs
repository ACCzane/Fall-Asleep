using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject countdownPanel;
    
    private void OnEnable() {
        EventHandler.Countdown += OnCountdown;
    }

    private void OnDisable() {
        EventHandler.Countdown -= OnCountdown;
    }

    public void TurnSettingPanel(){
        bool isActive = settingPanel.activeInHierarchy;
        settingPanel.SetActive(!isActive);      //控制设置面板的打开关闭
    }

    public void TurnCountdownText(){
        bool isActive = countdownPanel.activeInHierarchy;
        settingPanel.SetActive(!isActive);      //控制倒计时面板的打开关闭
    }

    public void UpdateCountdownText(int countdown){
        countdownText.text = "Left Time: \n" + countdown.ToString();
    }

    private void OnCountdown(int value)
    {
        UpdateCountdownText(value);
    }


}
