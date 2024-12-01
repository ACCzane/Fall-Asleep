using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Singleton;


    [SerializeField] private GameObject settingPanel;

    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private Image countDownBarImag;

    [SerializeField] private Image nightFallTranslationImage;
    
    [SerializeField] private Image healthBarImag;

    [SerializeField] private Image coinBarImag;

    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject successPanel;

    private bool openSettingPanel = false;
    private bool openCountdown;

    private void Awake() {

        if(Singleton == null){
            Singleton = this;
        }

        Time.timeScale = 1;
    }

    private void OnEnable() {
        // EventHandler.Countdown += OnCountdown;
        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        // EventHandler.Countdown -= OnCountdown;
        EventHandler.NightFall -= OnNightFall;
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


   

    // public void NightFallTranslateAsync(float fadein, float fadeout, float duration){
    //     StartCoroutine(NightFallTranslate(fadein,fadeout,duration));
    // }

    public IEnumerator FadeIn(float fadein, float duration){

        Color pureBlack = new Color(0,0,0,1);
        Color transparent = new Color(0,0,0,0);

        float elapsedTime = 0;
        while(elapsedTime < fadein){
            elapsedTime += Time.deltaTime;
            nightFallTranslationImage.color = Color.Lerp(transparent, pureBlack, elapsedTime/fadein);
            yield return null;
        }
        elapsedTime = 0;
        while(elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float fadeout){

        Color pureBlack = new Color(0,0,0,1);
        Color transparent = new Color(0,0,0,0);

        float elapsedTime = 0;
        while(elapsedTime < fadeout){
            // Debug.Log(elapsedTime);
            elapsedTime += Time.deltaTime;
            nightFallTranslationImage.color = Color.Lerp(pureBlack, transparent, elapsedTime/fadeout);
            yield return null;
        }
    }

    public void UpdatePlayerHealth(float playerHealthRate){
        healthBarImag.fillAmount = playerHealthRate;
    }

    public void UpdateTimecountBar(float timeCountRate){
        countDownBarImag.fillAmount = timeCountRate;
    }

    public void UpdateCoinBar(float coinRate){
        coinBarImag.fillAmount = coinRate;
        Debug.Log(coinRate);
    }

    public void TurnFailPanel(){
        failPanel.SetActive(true);
    }

    public void TurnSuccessPanel(){
        successPanel.SetActive(true);
    }

    private void OnNightFall()
    {
        countDownBarImag.fillAmount = 0;
    }

}
