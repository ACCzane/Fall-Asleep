using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject countdownPanel;
    
    private bool openSettingPanel;
    private bool openCountdown;

    private void OnEnable() {
        EventHandler.Countdown += OnCountdown;
    }

    private void OnDisable() {
        EventHandler.Countdown -= OnCountdown;
    }

    public void TurnSettingPanel(bool on){
        openSettingPanel = on;
        if(openSettingPanel){
            settingPanel.SetActive(true);      //控制倒计时面板的打开关闭
            Time.timeScale = 0;
        }else{
            settingPanel.SetActive(false);      //控制倒计时面板的打开关闭
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// 控制倒计时的开关
    /// </summary>
    public void TurnCountdownText(bool on){
        openCountdown = on;
        if(openCountdown){
            countdownPanel.SetActive(true);
        }else{
            countdownPanel.SetActive(true);
            countdownText.text = ""; 
        }
    }

    public void UpdateCountdownText(int countdown){
        countdownText.text = "Left Time: \n" + countdown.ToString();
    }

    private void OnCountdown(int value)
    {
        UpdateCountdownText(value);
    }


}
