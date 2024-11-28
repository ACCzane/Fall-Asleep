using UnityEngine;

public class Sheilf : MonoBehaviour, IInteractable
{
    // private Collider2D collider2D;

    [Header("参数")]
    [SerializeField] private Vector2 inPos;
    [SerializeField] private Vector2 outPos;

    private void Awake() {
        // collider2D = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //玩家可以按键进入
            other.GetComponent<Hide>().TurnHintButton(true);
            // other.GetComponent<PlayerControl>().currentActivatedInteractType = InteractType.Hide;
            other.GetComponent<PlayerControl>().currentInteractableTarget = this;

            //玩家注册进入位置和退出位置
            other.GetComponent<Hide>().SetPos(
                new Vector2(transform.position.x, transform.position.y) + inPos,
                new Vector2(transform.position.x, transform.position.y) + outPos
            );
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<Hide>().TurnHintButton(false);
            // other.GetComponent<PlayerControl>().currentActivatedInteractType = InteractType.Null;
            other.GetComponent<PlayerControl>().currentInteractableTarget = null;
        }
    }

    public void Interact(){
        EventHandler.CallHide();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + inPos, 0.1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + outPos, 0.1f);
    }
}
