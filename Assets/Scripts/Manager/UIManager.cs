using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    public void TurnSettingPanel(){
        bool isActive = settingPanel.activeInHierarchy;
        settingPanel.SetActive(!isActive);      //控制打开关闭
    }
}
