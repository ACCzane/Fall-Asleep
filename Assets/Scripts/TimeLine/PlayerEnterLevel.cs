using UnityEngine;

public class PlayerEnterLevel : MonoBehaviour
{
    [SerializeField] private Vector3 entryPos_1;
    [SerializeField] private Vector3 entryPos_2;

    [SerializeField] private PlayerControl playerControl;

    private bool isPlayerEntering;
    private Vector3 moveDir;

    private void Start() {
        moveDir = (entryPos_2 - entryPos_1).normalized;
    }

    private void Update() {
        if(isPlayerEntering){
            UpdatePlayerPos();
        }
    }

    public void StartPlayerEnterLevel(){
        isPlayerEntering = true;

        playerControl.transform.position = entryPos_1;
    }

    public void UpdatePlayerPos(){
        if(Vector3.Distance(playerControl.transform.position, entryPos_2) < 0.01f){
            isPlayerEntering = false;
        }
       
        playerControl.transform.Translate(moveDir * playerControl.PlayerSpeed * Time.deltaTime ,Space.World);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(entryPos_1, 0.5f);
        Gizmos.DrawWireSphere(entryPos_2, 0.5f);
    }
}
