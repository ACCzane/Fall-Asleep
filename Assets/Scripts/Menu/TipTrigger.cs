using UnityEngine;

public class TipTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] leftTip;
    [SerializeField] private GameObject[] righeTip;

    private void OnTriggerEnter2D(Collider2D other) {

    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            if(other.transform.position.x > transform.position.x){
                foreach (var item in leftTip)
                {
                    item.SetActive(false);
                }
                foreach (var item in righeTip)
                {
                    item.SetActive(true);
                }
            }else{
                foreach (var item in leftTip)
                {
                    item.SetActive(true);
                }
                foreach (var item in righeTip)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}
