using Cinemachine;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerFollowCam;
    [SerializeField] private bool leftRightDir;            //true表示左右方向，false表示上下，更新逻辑不同
    [SerializeField] private Collider2D L_U_Bound;
    [SerializeField] private Collider2D R_D_Bound;

    private void OnTriggerEnter2D(Collider2D other) {

    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            ChooseCam(other.transform);
        }
    }

    private void ChooseCam(Transform other){
        if(leftRightDir){
            if(other.position.x > transform.position.x){
                playerFollowCam.GetComponent<Cinemachine.CinemachineConfiner2D>().m_BoundingShape2D = R_D_Bound;
            }else{
                playerFollowCam.GetComponent<Cinemachine.CinemachineConfiner2D>().m_BoundingShape2D = L_U_Bound;
            }
        }
        else{
            if(other.position.y < transform.position.y){
                playerFollowCam.GetComponent<Cinemachine.CinemachineConfiner2D>().m_BoundingShape2D = R_D_Bound;
            }else{
                playerFollowCam.GetComponent<Cinemachine.CinemachineConfiner2D>().m_BoundingShape2D = L_U_Bound;
            }
        }
    }
}
