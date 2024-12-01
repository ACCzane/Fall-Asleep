using UnityEngine;

public class TipPresenter : MonoBehaviour
{          
    float timeCounter;
    int index;
    [SerializeField] private float singleTextDuration = 2f;
    [SerializeField] private GameObject[] texts;
    public void PlayFirstText()
    {
        timeCounter = 0;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > singleTextDuration){

            index++;
            index = index % texts.Length;
            timeCounter = 0;

            foreach (var item in texts)
            {
                item.SetActive(false);
            }
            texts[index].SetActive(true);

        }
    }
}
