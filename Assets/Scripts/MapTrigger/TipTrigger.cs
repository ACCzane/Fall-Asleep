using UnityEngine;

public class TipTrigger : MonoBehaviour
{

    [SerializeField] private bool leftRightDir;            //true表示左右方向，false表示上下，更新逻辑不同
    [SerializeField] private GameObject[] L_U_Tip;
    [SerializeField] private GameObject[] R_D_Tip;

    private void OnTriggerEnter2D(Collider2D other) {

    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            ChoosePlayTip(other.transform);
        }
    }

    private void ChoosePlayTip(Transform other){
        if(leftRightDir){
            if(other.position.x > transform.position.x){
                foreach (var item in L_U_Tip)
                {
                    item.SetActive(false);
                }
                foreach (var item in R_D_Tip)
                {
                    item.SetActive(true);
                    if(item.TryGetComponent<TipPresenter>(out TipPresenter tipPresenter)){
                        tipPresenter.PlayFirstText();
                    }
                }
            }else{
                foreach (var item in L_U_Tip)
                {
                    item.SetActive(true);
                    if(item.TryGetComponent<TipPresenter>(out TipPresenter tipPresenter)){
                        tipPresenter.PlayFirstText();
                    }
                }
                foreach (var item in R_D_Tip)
                {
                    item.SetActive(false);
                }
            }
        }
        else{
            if(other.position.y < transform.position.y){
                foreach (var item in L_U_Tip)
                {
                    item.SetActive(false);
                }
                foreach (var item in R_D_Tip)
                {
                    item.SetActive(true);
                    if(item.TryGetComponent<TipPresenter>(out TipPresenter tipPresenter)){
                        tipPresenter.PlayFirstText();
                    }
                }
            }else{
                foreach (var item in L_U_Tip)
                {
                    item.SetActive(true);
                    if(item.TryGetComponent<TipPresenter>(out TipPresenter tipPresenter)){
                        tipPresenter.PlayFirstText();
                    }
                }
                foreach (var item in R_D_Tip)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}
