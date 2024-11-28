using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    // private Collider2D collider2D;

    [Header("参数")]
    [SerializeField] private Vector2 inPos;
    [SerializeField] private Vector2 outPos;

    [SerializeField] private SpriteRenderer bedSpr;
    [SerializeField] private Sprite emptyBed;
    [SerializeField] private Sprite playerInBed;

    private bool canInteract;

    private void Awake() {
        // collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable() {
        EventHandler.Sleep += OnSleep;
    }

    private void OnDisable() {
        EventHandler.Sleep -= OnSleep;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //玩家可以按键进入
            other.GetComponent<Sleep>().TurnHintButton(true);
            // other.GetComponent<PlayerControl>().currentActivatedInteractType = InteractType.Sleep;
            other.GetComponent<PlayerControl>().currentInteractableTarget = this;

            //玩家注册进入位置和退出位置
            other.GetComponent<Sleep>().SetPos(
                new Vector2(transform.position.x, transform.position.y) + inPos,
                new Vector2(transform.position.x, transform.position.y) + outPos
            );
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<Sleep>().TurnHintButton(false);
            // other.GetComponent<PlayerControl>().currentActivatedInteractType = InteractType.Null;
            other.GetComponent<PlayerControl>().currentInteractableTarget = null;
        }
    }

    private void OnSleep()
    {
        if(canInteract)
        {
            bedSpr.sprite = playerInBed;
        }
        canInteract = false;
    }

    public void Interact(){
        EventHandler.CallSleep();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + inPos, 0.1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + outPos, 0.1f);
    }
}