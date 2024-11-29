using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour
{

    /// <summary>
    /// 加载场景，从1开始
    /// </summary>
    /// <param name="index"></param>
    public void EnterScene(int index){
        SceneManager.LoadScene(index);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
