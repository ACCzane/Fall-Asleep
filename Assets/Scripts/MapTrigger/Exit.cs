using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private int nextBuildIndex;

    public void EnterNextLevel(){
        SceneManager.LoadScene(nextBuildIndex);
    }

}
