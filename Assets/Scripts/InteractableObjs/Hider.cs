using UnityEngine;

public class Hider : MonoBehaviour
{
    // private Collider2D collider2D;

    [Header("参数")]
    [SerializeField] private Vector2 inPos;
    [SerializeField] private Vector2 outPos;

    private void Awake() {
        // collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //玩家可以按键进入
            other.GetComponent<Hide>().TurnHintButton(true);
            other.GetComponent<PlayerControl>().canPressE = true;

            //玩家注册进入位置和退出位置
            other.GetComponent<Hide>().SetPos(
                new Vector2(transform.position.x, transform.position.y) + inPos,
                new Vector2(transform.position.x, transform.position.y) + outPos
            );
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<Hide>().TurnHintButton(false);
            other.GetComponent<PlayerControl>().canPressE = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + inPos, 0.1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + outPos, 0.1f);
    }
}
